using DataRooms.Contracts;
using DataRooms.DataRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataRooms.WEBAPI.Infrastructure.Installers
{
    public class RegisterContractMappings : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration config)
        {
            services.AddTransient(typeof(ISqlRepository<>), typeof(SqlRepository<>));
        }
    }
}
