using System;
using System.Collections.Generic;
using System.Text;

namespace Bs.AutoCredito.Core.Entidades
{
  public class TablaAmortizacion
  {
        public int IdTablaAmortizacion { get; set; }
        public string IdOperacionCartera { get; set; }
        public int NumeroCuota { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Capital { get; set; }
        public decimal Interes { get; set; }
        public decimal CuotaTotal { get; set; }
        public decimal Saldo { get; set; }
        public decimal MontoPago { get; set; }
        public int DiasVencidos { get; set; }
        public decimal Mora { get; set; }

  }
}
