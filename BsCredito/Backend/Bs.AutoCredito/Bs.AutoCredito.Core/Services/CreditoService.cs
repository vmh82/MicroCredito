using Bs.AutoCredito.Core.Entidades;
using Bs.AutoCredito.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bs.AutoCredito.Core.Services
{
    public class CreditoService : ICreditoService
    {
        private readonly ICreditoRepository _creditoRepo;
        public CreditoService(ICreditoRepository creditoRepo)
        {
            _creditoRepo = creditoRepo;
        }

        public Task<IEnumerable<Credito>> ConsultarCreditoGenerado(string identificacion)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TablaAmortizacion>> ConsultarTablaAmortizacion(Credito credito)
        {
            return await _creditoRepo.ConsultarTablaAmortizacion(credito);
        }

        public async Task<int> GuardarCredito(Credito credito)
        {
            return await _creditoRepo.GuardarCredito(credito);
        }

        public async Task<int> PagarCuotaCredito(TablaAmortizacion tabla)
        {
            return await _creditoRepo.PagarCuotaCredito(tabla);
        }

        public async Task<IEnumerable<TablaAmortizacion>> ConsultarCuotaPendiente(string identificacion)
        {
            return await _creditoRepo.ConsultarCuotaPendiente(identificacion);
        }
    }
}
