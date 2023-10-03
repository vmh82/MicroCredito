using Bs.AutoCredito.Core.Interfaces;
using Bs.AutoCredito.Core.Services;
using Bs.AutoCredito.Infrastructure.Factory;
using Bs.AutoCredito.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bs.AutoCredito.IoC
{
    public static class ContainerConfig
    {

        public static void RegistrarServicios(IServiceCollection services)
        {
            services.AddScoped<IConnectionFactory, ConnectionFactory>();
            services.AddScoped<ICreditoService, CreditoService>();
            services.AddScoped<ICreditoRepository, CreditoRepository>();
        }
    }
}
