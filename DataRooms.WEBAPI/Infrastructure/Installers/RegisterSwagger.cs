using DataRooms.Contracts;
using DataRooms.WEBAPI.Infrastructure.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataRooms.WEBAPI.Infrastructure.Installers
{
    public class RegisterSwagger : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration config)
        {
            services.AddSwaggerGen(options => {
                    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "DataRooms Web Api", Version = "v1" });

                    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme { 
                        Scheme="Bearer",
                        Description = "Enter Following by space and JWT.",
                        Name = "Authorization",
                        Type=Microsoft.OpenApi.Models.SecuritySchemeType.Http
                     });

                options.OperationFilter<SwaggerAuthorizeCheckOperationFilter>();
            });
        }
    }
}
