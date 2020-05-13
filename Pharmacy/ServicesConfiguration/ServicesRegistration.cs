using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Pharmacy.Api.Services;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.HelpersInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrustructureInterfaces;
using Pharmacy.Application.Common.Mappings;
using Pharmacy.Application.Helpers;
using Pharmacy.Application.Services;
using Pharmacy.Infrastructure.Persistence.Repositories;
using Pharmacy.Infrastructure.Services;

namespace Pharmacy.Api.ServicesConfiguration
{
    public static class ServicesRegistration
    {
        public static void RegistereApplicationServices(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new PharmacyAppMapper());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);
            services.AddSingleton<IBlobStorage, BlobService>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IEmailHelper, EmailHelper>();
            services.AddTransient<ICookieHelper, CookieHelper>();
            services.AddTransient<IUserHelper, UserHelper>();
            services.AddTransient<ITokenHelper, TokenHelper>();
            services.AddTransient<IEmailSender, SendGridService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IUserManager, IdentityUserManager>();
            services.AddTransient<ISignInManager, IdentitySignInManager>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<IManufacturerService, ManufacturerService>();
            services.AddScoped<ICurrentUser, CurrentUserService>();

        }   
    }
}
