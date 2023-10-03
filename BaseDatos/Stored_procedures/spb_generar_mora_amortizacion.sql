use bs_credito
go
if exists(select 1 from sysobjects where name = 'spb_generar_mora_amortizacion')
drop procedure dbo.spb_generar_mora_amortizacion
go
create procedure dbo.spb_generar_mora_amortizacion
as begin


CREATE TABLE #tmp_operaciones_vencidas(
	id_opv int identity primary key,
	id_tabla_amortizacion int,
	fecha_pago date
)
INSERT INTO #tmp_operaciones_vencidas(id_tabla_amortizacion, fecha_pago)
SELECT ta_id_tabla_amortizacion, ta_fecha_pago  FROM  bs_credito.dbo.ca_tabla_amortizacion
WHERE ta_estado = 1


declare @w_numeroDiasMora int,
@w_int_contador int = 1,
@w_numerOperaciones int,
@w_id_tablaAmortizacion int,
@w_fecha_pago date,
@w_tasa_morocidad decimal(18,2)

select @w_numerOperaciones = count(*) from #tmp_operaciones_vencidas

while @w_int_contador <= @w_numerOperaciones
begin

	SELECT @w_fecha_pago =  fecha_pago FROM #tmp_operaciones_vencidas where id_tabla_amortizacion = @w_int_contador
	SELECT  @w_numeroDiasMora =  DATEDIFF(DAY,@w_fecha_pago  , '2021-04-16')from bs_credito.dbo.ca_tabla_amortizacion
	WHERE ta_id_tabla_amortizacion = @w_int_contador 

	IF @w_numeroDiasMora > 1 and @w_numeroDiasMora <= 15
	begin
		set @w_tasa_morocidad = 12 * 0.5
	end
	IF @w_numeroDiasMora > 16 and @w_numeroDiasMora <= 30
	begin
		set @w_tasa_morocidad = 12 * 0.7
	end
	IF @w_numeroDiasMora > 1 and @w_numeroDiasMora <= 15
	begin
		set @w_tasa_morocidad = 12 * 0.9
	end
	IF @w_numeroDiasMora > 31 and @w_numeroDiasMora <= 60
	begin
		set @w_tasa_morocidad = 12 * 0.5
	end
	IF @w_numeroDiasMora >= 60
	begin
		set @w_tasa_morocidad = 12 * 1.20
	end

	UPDATE ta
	set ta_dias_vencidos = @w_numeroDiasMora,
	ta_mora = (((ta_cuota_total * @w_tasa_morocidad) /360 * @w_numeroDiasMora)/100)
	FROM bs_credito.dbo.ca_tabla_amortizacion ta
	where ta_id_tabla_amortizacion = @w_int_contador

	set @w_int_contador  = @w_int_contador  + 1

 end


 end
