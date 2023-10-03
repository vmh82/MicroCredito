using System;
using System.Collections.Generic;
using Dapper;
using System.Text;
using System.Data;

namespace Bs.AutoCredito.Infrastructure.Factory
{
    public class SqlParametrosDapper
    {
        public string Name;
        public DbType? Type;
        public int Size;
        public object Value;
        public ParameterDirection Direction;
        public byte Precision;
        public byte Scale;
        public bool IsTypeTable;

        /// <summary>
        /// Permite crear un nuevo parametro
        /// </summary>
        /// <param name="parametro"></param>
        /// <param name="tamaño"></param>
        public SqlParametrosDapper(string parametro, int tamaño)
        {
            Name = parametro;
            Size = tamaño;
            Value = null;
            Direction = ParameterDirection.Input;
            Precision = 0;
            Scale = 0;
            Type = null;
        }


        /// <summary>
        /// Permite crear un nuevo parametro
        /// </summary>
        /// <param name="parametro"></param>
        /// <param name="tipo"></param>
        /// <param name="esTipoTabla"></param>
        public SqlParametrosDapper(string parametro, DataTable tipo, bool esTipoTabla)
        {
            Name = parametro;
            Value = tipo;
            Direction = ParameterDirection.Input;
            Precision = 0;
            Scale = 0;
            Type = null;
            IsTypeTable = esTipoTabla;
        }

        /// <summary>
        /// Permite crear un nuevo parametro
        /// </summary>
        /// <param name="parametro"></param>
        /// <param name="tipo"></param>
        /// <param name="tamaño"></param>

        public SqlParametrosDapper(string parametro, DbType? tipo, int tamaño)
        {
            Name = parametro;
            Size = tamaño;
            Value = null;
            Direction = ParameterDirection.Input;
            Precision = 0;
            Scale = 0;
            Type = tipo;
        }

        /// <summary>
        /// Permite crear un nuevo parametro
        /// </summary>
        /// <param name="parametro"></param>
        /// <param name="tipo"></param>
        /// <param name="tamaño"></param>
        /// <param name="valorParametro"></param>
        public SqlParametrosDapper(string parametro, DbType tipo, int tamaño, object valorParametro)
        {
            Name = parametro;
            Size = tamaño;
            Value = valorParametro;
            Direction = ParameterDirection.Input;
            Precision = 0;
            Scale = 0;
            Type = tipo;
        }

    }
}
