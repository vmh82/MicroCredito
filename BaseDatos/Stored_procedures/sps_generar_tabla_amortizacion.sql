use bs_credito
go
if exists(select 1 from sysobjects where name = 'sps_generar_tabla_amortizacion')
drop procedure dbo.sps_generar_tabla_amortizacion
go
create procedure dbo.sps_generar_tabla_amortizacion
(
	@i_monto_solicitado decimal(18,4),
	@i_plazo int	
)as begin

	declare 
	@w_tiempo  decimal(10,8),
	@w_cuota decimal(18,2),
	@w_capital decimal(18,2) = 0,
	@w_interes decimal(18,2) = 0,
	@w_capital_interes decimal(18,2) = 0,
	@w_saldo_cap decimal(18,2) = 0,
	@w_saldo_desgrav decimal(8,4) = 0,
	@w_saldo_cuota_desgravamen decimal(18,2) = 0,
	@w_capital_pendiente decimal(18,2) = 0,
	@w_interes_calculado decimal(18,2) = 0,
	@w_tasa_interes decimal(18,2),
	@w_amortizacion decimal(18,2),
	@w_desgravamen decimal(18,2),
	@w_desgravamen_calculado decimal(18,2),
	@w_cuota_desgravamen decimal(18,2),
	@w_fecha_actual date,
	@w_fecha_estimada_pago date,
	@w_fecha_aux date,
	@w_contador_inicio int = 1

	create table #tmp_tabla_amortizacion
	(
		ta_id_tabla_amortizacion int identity(1,1) primary key,
		ta_num_cuota int,
		ta_fecha_pago date,
		ta_capital decimal(18,2),
		ta_interes decimal(18,2),
		ta_capital_interes decimal(18,2),
		ta_saldo_capital decimal(18,2),
		ta_desgravamen decimal(18,2),
		ta_cuota decimal(18,2),
	)


	select @w_tasa_interes = pc_tasa_interes, @w_desgravamen = pc_valor_desgravamen from ca_parametros_credito


	set @w_tiempo =(@w_tasa_interes/100)/12; 
	set @w_capital_pendiente = @i_monto_solicitado
	set @w_fecha_actual= getdate()
	set @w_fecha_aux = getdate()
	while @w_contador_inicio <= @i_plazo
	begin
		set @w_fecha_actual  = dateadd(month, 1, @w_fecha_aux);
		set @w_cuota =  floor((@i_monto_solicitado * @w_tiempo)/(1-power(1+@w_tiempo,(-1*@i_plazo)))*100)/100 --calculo cuota
		set @w_interes_calculado = (@w_capital_pendiente * @w_tiempo *100)/100
		set @w_amortizacion = round(((@w_cuota - @w_interes_calculado)*100),1)/100
		set @w_capital_pendiente = round(((@w_capital_pendiente - @w_amortizacion)*100),1)/100
		set @w_desgravamen_calculado  = round((((@w_capital_pendiente * @w_desgravamen)*100)/100),2)
		set @w_cuota_desgravamen = round((((@w_cuota + @w_desgravamen_calculado)*100)/100),2,1)
		set @w_capital =  @w_capital + @w_amortizacion
		set @w_interes =  @w_interes+ @w_interes_calculado
		set @w_capital_interes = @w_capital_interes + @w_cuota
		set @w_saldo_cap = @w_saldo_cap  +@w_capital_pendiente
		set @w_saldo_desgrav = @w_saldo_desgrav + @w_desgravamen_calculado
		set @w_saldo_cuota_desgravamen = @w_saldo_cuota_desgravamen + @w_cuota_desgravamen
		set @w_fecha_aux = @w_fecha_actual

		insert into #tmp_tabla_amortizacion values(@w_contador_inicio, @w_fecha_actual, @w_amortizacion, @w_interes_calculado, @w_cuota, @w_capital_pendiente, @w_capital_pendiente, @w_capital_pendiente)
		set @w_contador_inicio  = @w_contador_inicio  + 1
	end
	
	select ta_num_cuota 'NumeroCuota', ta_fecha_pago 'FechaPago',
		   ta_capital 'Capital', ta_interes 'Interes', 
		   ta_capital_interes 'CuotaTotal', ta_cuota 'Saldo' from #tmp_tabla_amortizacion
	
	
end
