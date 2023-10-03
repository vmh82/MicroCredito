using System;
using System.Collections.Generic;
using System.Text;

namespace Bs.AutoCredito.Core.Entidades
{
    public class Credito
    {
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string Identificacion { get; set; }
        public decimal MontoSolicitado { get; set; }
        public int Plazo { get; set; }
        public List<TablaAmortizacion> TablaAmortizacion { get; set; }
    }

}
