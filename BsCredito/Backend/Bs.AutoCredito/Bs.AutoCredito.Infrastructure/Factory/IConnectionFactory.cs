using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bs.AutoCredito.Infrastructure.Factory
{
    public interface IConnectionFactory
    {
        string CadenaConexion { get; set; }

        /// <summary>
        /// Trasmite las sentencias de consulta hacia el motor de base de datos.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parametrosEjecucion"></param>
        /// <returns>Entidad</returns>
        Task<IEnumerable<T>> ExecuteDataSet<T>(ParametrosEjecucion parametrosEjecucion);

        /// <summary>
        /// Trasmite las sentencias de consulta hacia el motor de base de datos.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Entidad</returns>
        Task<IEnumerable<T>> ExecuteQuery<T>(StringBuilder query);

        /// <summary>
        /// Trasmite las sentencias de creacion, actualizacion o eliminacion
        /// hacia el motor de base de datos.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Entidad</returns>
        Task<int> ExecuteNonQuery(ParametrosEjecucion parametrosEjecucion);
        Task<dynamic> ExecuteReader(ParametrosEjecucion parametrosEjecucion);
    }
}
