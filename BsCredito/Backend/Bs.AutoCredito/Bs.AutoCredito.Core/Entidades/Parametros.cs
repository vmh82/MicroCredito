using System;
using System.Collections.Generic;
using System.Text;

namespace Bs.AutoCredito.Core.Entidades
{
    public class Parametros
    {
        public double Interes { get; set; }
        public double Desgravamen { get; set; }
        public List<int>Plazo { get; set; }
    }
}
