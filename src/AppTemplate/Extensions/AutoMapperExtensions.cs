using AppTemplate.Profiles;

namespace AppTemplate.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection AddProfiles(this IServiceCollection service)
        {
            service.AddAutoMapper(
                typeof(UsernameAndPasswordProfile)
            );

            return service;
        }
    }
}
