use bs_credito
exec spi_crear_operacion_cartera '1724389745','Victor Fabian', 'Maldonado Hernandez', 1000, 12
exec sps_generar_tabla_amortizacion 1000, 12

exec sps_consultar_cuota_pendiente '1724389745'

exec spu_actualizar_saldo_cuota '1724389745', 'OCR-001', '1', '0.20'

truncate table  ca_operacion_cartera
truncate table   ca_tabla_amortizacion

select * from ca_operacion_cartera
select * from ca_tabla_amortizacion
