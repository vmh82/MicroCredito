use bs_credito
go
IF EXISTS(SELECT 1 FROM sysobjects where name = 'spu_actualizar_saldo_cuota')
DROP PROCEDURE dbo.spu_actualizar_saldo_cuota
GO
CREATE PROCEDURE dbo.spu_actualizar_saldo_cuota
(
	@i_codigo_operacion_cartera varchar(500),
	@i_codigo_tabla_amortizacion int,
	@i_monto_pagado decimal(18,2)
)AS BEGIN

	declare @w_fecha_actual DATETIME,
	@cuota_total decimal(18,2)
	set @w_fecha_actual = GETDATE()

	select @cuota_total = ta_cuota_total from ca_tabla_amortizacion  ta
	where ta_codigo_operacion_cartera = @i_codigo_operacion_cartera
	and ta_id_tabla_amortizacion = @i_codigo_tabla_amortizacion
	and ta_estado = 1

	if @cuota_total = @i_monto_pagado
	BEGIN --PAGO TOTAL
			
		update ta
		set ta_monto_pago = @i_monto_pagado,
		ta_saldo_actual = ta_cuota_total - @i_monto_pagado,
		ta_cuota_total = ta_cuota_total - @i_monto_pagado,
		ta_estado = 0,
		ta_fecha_aplicacion_pago = @w_fecha_actual
		from ca_tabla_amortizacion  ta
		where ta_codigo_operacion_cartera = @i_codigo_operacion_cartera
		and ta_id_tabla_amortizacion = @i_codigo_tabla_amortizacion
		and ta_estado = 1

		SELECT 1
	END
	ELSE
	BEGIN
		--PAGO PARCIAL
		update ta
		set ta_monto_pago = @i_monto_pagado,
		ta_saldo_actual = ta_cuota_total - @i_monto_pagado,
		ta_cuota_total = ta_cuota_total - @i_monto_pagado,
		ta_fecha_aplicacion_pago = @w_fecha_actual
		from ca_tabla_amortizacion  ta
		where ta_codigo_operacion_cartera = @i_codigo_operacion_cartera
		and ta_id_tabla_amortizacion = @i_codigo_tabla_amortizacion
		and ta_estado = 1

		SELECT 1
	END
END