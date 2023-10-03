

create table ca_operacion_cartera(
 oc_id_operacion_cartera int identity(1,1) primary key,
 oc_codigo_operacion varchar(500),
 oc_identificacion_cliente varchar(30) null,
 oc_nombres_cliente varchar(250) null,
 oc_apellidos_cliente varchar(250) null,
 oc_monto_solicitado decimal(18,2),
 oc_monto_aprobado decimal(18,2),
 oc_monto_desembolsado decimal(18,2),
 oc_plazo int,
 oc_interes decimal(18,2),
 oc_desgravamen decimal(18,2),
 oc_estado smallint,
 oc_fecha_creacion datetime,
 oc_fecha_cancelacion datetime,
)
go
 create table ca_tabla_amortizacion(
  ta_id_tabla_amortizacion int identity(1,1) primary key,
  ta_codigo_operacion_cartera varchar(500),
  ta_num_cuota int,
  ta_capital decimal(18,2),
  ta_interes decimal(18,2),
  ta_cuota_total decimal(18,2),
  ta_saldo decimal(18,2),
  ta_monto_pago decimal(18,2),
  ta_saldo_actual decimal(18,2),
  ta_fecha_creacion datetime,
  ta_fecha_pago date,
  ta_fecha_aplicacion_pago datetime,
  ta_fecha_modificacion datetime,
  ta_estado smallint
)
go
create table ca_parametros_credito(
  pc_id_parametros int identity(1,1) primary key,
  pc_tasa_interes decimal(18,2),
  pc_plazo_minimo int,
  pc_plazo_maximo int,
  pc_valor_desgravamen decimal(18,2)
)
go
