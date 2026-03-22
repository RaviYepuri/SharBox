using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataRooms.Contracts
{
    public interface IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration config);
    }
}
