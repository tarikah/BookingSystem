using BookingSystem.InMemory;
using BookingSystem.Interfaces.Contracts;
using BookingSystem.Interfaces.Factories;
using BookingSystem.Services.Factory;
using BookingSystem.Services.Implementations;
using BookingSystem.Tripx;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace BookingSystem.DependencyInjection
{
    public static class Services
    {
        #region Private
        private static void AddApplication(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<ISearchService, SearchService>();
            builder.Services.AddSingleton<IDestinationSearchFactory, BaseDestionationSearchFactory>();
            builder.Services.AddSingleton<IBookingService, BookingService>();
        }

        private static void AddInfrastructure(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IBaseRepository, InMemoryRepository>();
            builder.Services.AddSingleton<IAccountService, AccountService>();
            builder.Services.AddSingleton<ITripxClient, TripxClient>();
            builder.Services.AddHttpClient("tripx", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration.GetSection("TripxAPI").Value);
            });

            //We can register the http client this way also, and use HttpClient directly in the TripXClient service

            //builder.Services.AddHttpClient<ITripxClient, TripxClient>(client =>
            //{
            //    client.BaseAddress = new Uri(builder.Configuration.GetSection("TripxAPI").Value);
            //});




        }

        private static void AddSwagger(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }

        private static void AddAuthentication(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                            .GetBytes(builder.Configuration.GetSection("JwtTokenKey").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }
        #endregion

        public static void AddDependencies(this WebApplicationBuilder builder)
        {
            AddApplication(builder);
            AddAuthentication(builder);
            AddInfrastructure(builder);
            AddSwagger(builder);
        }
    }

}
