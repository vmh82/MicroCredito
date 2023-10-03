using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;


namespace Bs.AutoCredito.Infrastructure.Factory
{
    public class ConnectionFactory : IConnectionFactory
    {
        private SqlParametrosDapper[] parametrosSalida;
        private readonly IConfiguration _configuration;
        private string cadenaConexion = string.Empty;
        private static Dictionary<string, string> _dictCadenaConexion = new Dictionary<string, string>();
        private object _lock = new object();
        private int numeroParametrosOut;


        public ConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public SqlParametrosDapper[] OutParametro
        {
            get
            {
                return (SqlParametrosDapper[])parametrosSalida.Clone();
            }
            set
            {
                parametrosSalida = value;
            }
        }

        /// <summary>
        /// Verifica si la cadenade conexion existe y devuelve sus parametros.
        /// </summary>
        public string CadenaConexion
        {
            get
            {
                return cadenaConexion;
            }
            set
            {
                if (!_dictCadenaConexion.ContainsKey(value))
                {
                    lock (_lock)
                    {
                        if (!_dictCadenaConexion.ContainsKey(value))
                        {
                            if (_configuration.GetConnectionString(value) != null)
                            {
                                _dictCadenaConexion.Add(value, _configuration.GetConnectionString(value));
                            }
                            else
                            {
                                throw new ArgumentException(string.Format("Error Cadena de Conexion"), value);
                            }
                        }
                    }
                }

                cadenaConexion = _dictCadenaConexion[value];
            }
        }

        /// <summary>
        /// Permite verificar los tipos de datos enviados y crear un sql parameter
        /// generico.
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns></returns>
        private DynamicParameters AgregarParametrosConexion(SqlParametrosDapper[] parametros)
        {

            try
            {
                DynamicParameters parametrosDapper = new DynamicParameters();
                if (parametros != null)
                {
                    foreach (SqlParametrosDapper parametro in parametros)
                    {
                        if (parametro.IsTypeTable)
                        {
                            parametrosDapper.Add(parametro.Name, ((DataTable)parametro.Value).AsTableValuedParameter());
                        }
                        else
                        {
                            if (parametro.Type == DbType.Decimal)
                            {
                                parametro.Precision = 28;
                                parametro.Scale = 4;
                            }

                            parametrosDapper.Add(parametro.Name, parametro.Value, dbType: parametro.Type,
                                                direction: parametro.Direction, size: parametro.Size,
                                                precision: parametro.Precision, scale: parametro.Scale);

                            if (parametro.Direction == ParameterDirection.Output)
                            {
                                numeroParametrosOut++;
                            }
                            else if (parametro.Direction == ParameterDirection.ReturnValue)
                            {
                                parametro.Value = -999;
                            }
                        }
                    }
                }

                return parametrosDapper;

            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        ///Trasmite las sentencias de consulta hacia el motor de base de datos.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parametrosEjecucion"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ExecuteDataSet<T>(ParametrosEjecucion parametrosEjecucion)
        {
            IEnumerable<T> listaRetorno;
            try
            {
                using (IDbConnection conexion = new SqlConnection(cadenaConexion))
                {
                    DynamicParameters parametros = new DynamicParameters();
                    parametros = AgregarParametrosConexion(parametrosEjecucion.DapperParametros);
                    listaRetorno = await conexion.QueryAsync<T>(parametrosEjecucion.NombreProcedimiento, parametros, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return listaRetorno;
        }

        /// <summary>
        ///Trasmite  sentencia simples de consulta hacia el motor de base de datos.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parametrosEjecucion"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ExecuteQuery<T>(StringBuilder query)
        {
            IEnumerable<T> listaRetorno;
            try
            {
                using (IDbConnection conexion = new SqlConnection(cadenaConexion))
                {
                    DynamicParameters parametros = new DynamicParameters();
                    listaRetorno = await conexion.QueryAsync<T>(query.ToString());
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return listaRetorno;
        }

        public async Task<int> ExecuteNonQuery(ParametrosEjecucion parametrosEjecucion)
        {
            try
            {
                using (IDbConnection conexion = new SqlConnection(cadenaConexion))
                {
                    DynamicParameters parametros = new DynamicParameters();
                    parametros = AgregarParametrosConexion(parametrosEjecucion.DapperParametros);
                    return await conexion.QuerySingleAsync<int>(parametrosEjecucion.NombreProcedimiento, parametros, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<dynamic> ExecuteReader(ParametrosEjecucion parametrosEjecucion)
        {
            try
            {
                using (IDbConnection conexion = new SqlConnection(cadenaConexion))
                {
                    DynamicParameters parametros = new DynamicParameters();
                    parametros = AgregarParametrosConexion(parametrosEjecucion.DapperParametros);
                    return await conexion.QueryAsync(parametrosEjecucion.NombreProcedimiento, parametros, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
