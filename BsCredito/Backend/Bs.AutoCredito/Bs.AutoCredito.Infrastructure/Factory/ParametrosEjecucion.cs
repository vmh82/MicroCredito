using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Bs.AutoCredito.Infrastructure.Factory
{
    public class ParametrosEjecucion
    {
        public string NombreProcedimiento { get; set; }
        public SqlParametrosDapper[] DapperParametros { get; set; }
        public DataTable DtParametrosEntrada { get; set; }
    }
}
