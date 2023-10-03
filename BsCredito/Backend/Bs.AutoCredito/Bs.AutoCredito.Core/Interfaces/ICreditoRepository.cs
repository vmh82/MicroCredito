using Bs.AutoCredito.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bs.AutoCredito.Core.Interfaces
{
    public interface ICreditoRepository
    {
        public Task<IEnumerable<Credito>> ConsultarCreditoGenerado(string identificacion);
        public Task<IEnumerable<TablaAmortizacion>> ConsultarTablaAmortizacion(Credito credito);
        public Task<int> GuardarCredito(Credito credito);
        public Task<int> PagarCuotaCredito(TablaAmortizacion credito);

        public Task<IEnumerable<TablaAmortizacion>> ConsultarCuotaPendiente(string identificacion);
    }
}
