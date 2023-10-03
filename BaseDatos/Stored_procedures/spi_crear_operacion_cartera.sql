use bs_credito
go
IF EXISTS(SELECT 1 FROM sysobjects where name = 'spi_crear_operacion_cartera')
DROP PROCEDURE dbo.spi_crear_operacion_cartera
GO
CREATE PROCEDURE dbo.spi_crear_operacion_cartera
(
	@i_identificacion varchar(30),
	@i_nombres varchar(250),
	@i_apellidos varchar(250),
	@i_monto_solicitado money,
	@i_plazo int
)AS BEGIN

DECLARE @Rows int

	declare @w_secuencialTran varchar(max),
	@w_interes decimal(18,2),
	@w_desgravamen decimal(18,2) --secuencial operacion_cartera 

	create table #tmp_tabla_amortizacion_cartera
	(
		ta_id_tabla_amortizacion int identity primary key,
		ta_num_cuota int,
		ta_fecha_pago date,
		ta_pago_capital decimal(18,2),
		ta_pago_interes decimal(18,2),
		ta_cuota_total decimal(18,2),
		ta_saldo decimal(18,2)
	)


	select @w_interes = pc_tasa_interes, @w_desgravamen = pc_valor_desgravamen from ca_parametros_credito

	insert into #tmp_tabla_amortizacion_cartera(ta_num_cuota, ta_fecha_pago, ta_pago_capital, ta_pago_interes, ta_cuota_total, ta_saldo)
	exec sps_generar_tabla_amortizacion @i_monto_solicitado, @i_plazo

	select @w_secuencialTran = max(cast(substring(oc_codigo_operacion, charindex('-', oc_codigo_operacion) + 2, len(oc_codigo_operacion)) as int)) + 1 
	from ca_operacion_cartera 

	IF LEN(@w_secuencialTran) > 0
	BEGIN
		SET @w_secuencialTran = 'OCR-' + '00' + CAST(@w_secuencialTran AS nvarchar)
	END
	ELSE
	BEGIN
		SET @w_secuencialTran = 'OCR-' + '001'
	END


	insert into ca_operacion_cartera(oc_codigo_operacion, oc_identificacion_cliente, oc_nombres_cliente, 
	oc_apellidos_cliente, oc_monto_solicitado, oc_monto_aprobado, oc_monto_desembolsado, oc_plazo, oc_interes, oc_desgravamen,
	oc_estado, oc_fecha_creacion)  
	values(@w_secuencialTran, @i_identificacion, @i_nombres, @i_apellidos, @i_monto_solicitado,
	@i_monto_solicitado, @i_monto_solicitado, @i_plazo, @w_interes, @w_desgravamen, 1, GETDATE())


	insert into ca_tabla_amortizacion(ta_codigo_operacion_cartera, ta_num_cuota, ta_fecha_pago, ta_capital,
	ta_interes, ta_cuota_total,ta_saldo, ta_fecha_creacion, ta_estado)
	select @w_secuencialTran, ta_num_cuota, ta_fecha_pago, ta_pago_capital, ta_pago_interes,
	ta_cuota_total, ta_saldo, getdate(), 1  from #tmp_tabla_amortizacion_cartera

	SELECT @@ROWCOUNT


END