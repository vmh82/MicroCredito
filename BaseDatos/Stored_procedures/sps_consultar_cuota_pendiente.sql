use bs_credito
go
IF EXISTS(SELECT 1 FROM sysobjects where name = 'sps_consultar_cuota_pendiente')
DROP PROCEDURE dbo.sps_consultar_cuota_pendiente
GO
CREATE PROCEDURE dbo.sps_consultar_cuota_pendiente
(
	@i_identificacion varchar(30)
)AS BEGIN

	declare @w_fecha_actual DATE
	set @w_fecha_actual = GETDATE()

	select @w_fecha_actual  = '2021-04-12'--Para ejemplo activar en caso que se desee ver una cuota pendiente.

	create table #tmp_operaciones
	(
		oc_id_operacones int identity primary key,
		oc_codigo_operacion varchar(500)
	)
	insert into #tmp_operaciones(oc_codigo_operacion)
	select oc_codigo_operacion from ca_operacion_cartera
	where oc_identificacion_cliente = @i_identificacion
	and oc_estado = 1

	select ta_id_tabla_amortizacion 'IdTablaAmortizacion',ta_codigo_operacion_cartera as 'IdOperacionCartera', ta_num_cuota 'NumeroCuota',
	ta_fecha_pago 'FechaPago', ta_capital 'Capital', ta_interes 'Interes',  (ta_cuota_total + ta_mora) as 'CuotaTotal', ta_saldo 'Saldo',
	ta_dias_vencidos 'DiasVencidos', ta_mora 'Mora'
	from ca_tabla_amortizacion
	inner join #tmp_operaciones on oc_codigo_operacion = ta_codigo_operacion_cartera
	and ta_fecha_pago = @w_fecha_actual
	and ta_estado = 1

END