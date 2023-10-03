using Bs.AutoCredito.Core.Entidades;
using Bs.AutoCredito.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bs.AutoCredito.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CreditoController : ControllerBase
  {
        private readonly ICreditoService _crService;

        public CreditoController(ICreditoService crService)
        {
            _crService = crService;
        }


        [HttpGet]
        [Route("ConsultarParametros")]
        public ActionResult ConsultarParametros()
        {
            var secuencia = Enumerable.Range(1, 36).ToList();

            Parametros parametros = new Parametros
            {
                Desgravamen = 2.22,
                Interes = 12,
                Plazo = secuencia
            };

            return Ok(parametros);
        }


        [HttpPost]
        [Route("ConsultarTablaAmortizacion")]
        public async Task<ActionResult> ConsultarTablaAmortizacion(Credito credito)
        {
            return Ok(await _crService.ConsultarTablaAmortizacion(credito));
        }

        [HttpPost]
        [Route("GuardarCredito")]
        public async Task<ActionResult> GuardarCredito(Credito credito)
        {
            int resultado = await _crService.GuardarCredito(credito);
            if(resultado > 0)
            {
                return Ok("Credito guardado exitosamente");
            }
            else
            {
                return Ok("Ocurrio un error al registrar el credito");
            }
           
        }

        [HttpPost]
        [Route("PagarCuotaCredito")]
        public async Task<ActionResult> PagarCuotaCredito(TablaAmortizacion tabla)
        {
            int resultado = await _crService.PagarCuotaCredito(tabla);
            if (resultado > 0)
            {
                return Ok("Cuota pagada exitosamente");
            }
            else
            {
                return Ok("Ocurrio un error al registrar el pago de la cuota");
            }
        }


        [HttpPost]
        [Route("ConsultarCuotaPendiente")]
        public async Task<ActionResult> ConsultarCuotaPendiente([FromBody] string identificacion)
        {
            IEnumerable<TablaAmortizacion> cuotasPendientes = await _crService.ConsultarCuotaPendiente(identificacion);
            if (cuotasPendientes.Count() > 0)
            {
                return Ok(cuotasPendientes);
            }
            else
            {
                return Ok(cuotasPendientes);
            }
        }

    }
}
