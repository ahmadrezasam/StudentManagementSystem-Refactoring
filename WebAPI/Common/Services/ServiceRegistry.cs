using API.Authorization;
using API.Common.Interfaces;

namespace API.Common.Services
{
    public static class ServiceRegistry
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IInstructorAuthRepository, InstructorAuthRepository>();
            // services.AddScoped<IInstructorAuthRepository, StudentAuthRepository>();
            services.AddScoped<IJWTTokenGenerator, JWTTokenGenerator>();
        }
    }
}