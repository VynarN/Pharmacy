using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Pharmacy.Api.Services;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.HelpersInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Application.Common.Mappings;
using Pharmacy.Application.Common.Queries;
using Pharmacy.Application.Helpers;
using Pharmacy.Application.Services;
using Pharmacy.Domain.Entites;
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
            services.AddTransient<ICookieService, CookieService>();
            services.AddTransient<IUserHelper, UserHelper>();
            services.AddTransient<ITokenHelper, TokenHelper>();
            services.AddTransient<IEmailSender, SendGridService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IUserManager, IdentityUserManager>();
            services.AddTransient<ISignInManager, IdentitySignInManager>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<IDeliveryAddressService, DeliveryAddressService>();
            services.AddTransient<IManufacturerService, ManufacturerService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IMedicamentService, MedicamentService>();
            services.AddTransient<IInstructionService, InstructionService>();
            services.AddTransient<IAllowedForEntityService, AllowedForEntityService>();
            services.AddTransient<IApplicationMethodService, ApplicationMethodService>();
            services.AddTransient<IMedicamentFormService, MedicamentFormService>();
            services.AddTransient<IAllowedForEntityService, AllowedForEntityService>();
            services.AddTransient<IPaginationService, PaginationService>();
            services.AddTransient<IFilterHelper<Medicament, MedicamentFilterQuery>, MedicamentFilterHelper>();
            services.AddScoped<IUriService, UriService>();
            services.AddScoped<ICurrentUser, CurrentUserService>();
        }   
    }
}
