using Bs.AutoCredito.Comun;
using Bs.AutoCredito.Core.Entidades;
using Bs.AutoCredito.Core.Interfaces;
using Bs.AutoCredito.Infrastructure.Factory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Bs.AutoCredito.Infrastructure.Repository
{
    public class CreditoRepository : ICreditoRepository
    {
        private readonly IConnectionFactory _context;
        public CreditoRepository(IConnectionFactory context)
        {
            _context  = context;

        }
        public Task<IEnumerable<Credito>> ConsultarCreditoGenerado(string identificacionCliente)
        {
            throw new NotImplementedException();
        }

        

        public async Task<IEnumerable<TablaAmortizacion>> ConsultarTablaAmortizacion(Credito credito)
        {
            try
            {
                _context.CadenaConexion = Constantes.CadenaConexionBsCredito;;
                ParametrosEjecucion parametrosEjecucion = new ParametrosEjecucion
                {
                    NombreProcedimiento = Constantes.sps_generar_tabla_amortizacion,
                    DapperParametros = new[]{
                         new SqlParametrosDapper("@i_monto_solicitado", DbType.Decimal, 0, credito.MontoSolicitado),
                         new SqlParametrosDapper("@i_plazo", DbType.Int64, 999, credito.Plazo),
                    }
                };
                return await _context.ExecuteDataSet<TablaAmortizacion>(parametrosEjecucion);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> GuardarCredito(Credito credito)
        {
            try
            {
                _context.CadenaConexion = Constantes.CadenaConexionBsCredito; ;
                ParametrosEjecucion parametrosEjecucion = new ParametrosEjecucion
                {
                    NombreProcedimiento = Constantes.spi_crear_operacion_cartera,
                    DapperParametros = new[]{
                         new SqlParametrosDapper("@i_identificacion", DbType.String, 30, credito.Identificacion),
                         new SqlParametrosDapper("@i_nombres", DbType.String, 250, credito.NombreCliente),
                         new SqlParametrosDapper("@i_apellidos", DbType.String, 250, credito.ApellidoCliente),
                         new SqlParametrosDapper("@i_monto_solicitado", DbType.Decimal, 0, credito.MontoSolicitado),
                         new SqlParametrosDapper("@i_plazo", DbType.Int64, 999, credito.Plazo),
                    }
                };
                return await _context.ExecuteNonQuery(parametrosEjecucion);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> PagarCuotaCredito(TablaAmortizacion tabla)
        {
            try
            {
                _context.CadenaConexion = Constantes.CadenaConexionBsCredito; ;
                ParametrosEjecucion parametrosEjecucion = new ParametrosEjecucion
                {
                    NombreProcedimiento = Constantes.spu_actualizar_saldo_cuota,
                    DapperParametros = new[]{
                         new SqlParametrosDapper("@i_codigo_operacion_cartera", DbType.String, 500, tabla.IdOperacionCartera),
                         new SqlParametrosDapper("@i_codigo_tabla_amortizacion", DbType.Int64,999, tabla.IdTablaAmortizacion),
                         new SqlParametrosDapper("@i_monto_pagado", DbType.Decimal, 0, tabla.MontoPago),
                    }
                };
                return await _context.ExecuteNonQuery(parametrosEjecucion);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<TablaAmortizacion>> ConsultarCuotaPendiente(string identificacion)
        {
            try
            {
                _context.CadenaConexion = Constantes.CadenaConexionBsCredito; ;
                ParametrosEjecucion parametrosEjecucion = new ParametrosEjecucion
                {
                    NombreProcedimiento = Constantes.sps_consultar_cuota_pendiente,
                    DapperParametros = new[]{
                         new SqlParametrosDapper("@i_identificacion", DbType.String, 30, identificacion),
                    }
                };
                return await _context.ExecuteDataSet<TablaAmortizacion>(parametrosEjecucion);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
