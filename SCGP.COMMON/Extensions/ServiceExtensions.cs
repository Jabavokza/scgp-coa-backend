using SCGP.COA.COMMON.Attributes;

namespace SCGP.COA.COMMON.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Define types that need matching
            Type scopedRegistration = typeof(ScopedRegistrationAttribute);
            Type scopedPriorityRegistration = typeof(ScopedPriorityRegistrationAttribute);
            Type singletonRegistration = typeof(SingletonRegistrationAttribute);
            Type transientRegistration = typeof(TransientRegistrationAttribute);
            Type transientPriorityRegistration = typeof(TransientPriorityRegistrationAttribute);

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => 
                (
                    p.IsDefined(scopedRegistration, true) 
                    || p.IsDefined(transientRegistration, true) 
                    || p.IsDefined(singletonRegistration, true)
                    || p.IsDefined(scopedPriorityRegistration, true)
                    || p.IsDefined(transientPriorityRegistration, true)
                )
                && !p.IsInterface).Select(s => new
                {
                    Service = s.GetInterface($"I{s.Name}"),
                    Implementation = s
                }).Where(x => x.Service != null);

            var scopedPriorityTypes = types.Where(type => type.Implementation.IsDefined(scopedPriorityRegistration, false)).ToList();
            foreach (var type in scopedPriorityTypes)
            {
                services.AddScoped(type.Service, type.Implementation);
            }
            var transientPriorityTypes = types.Where(type => type.Implementation.IsDefined(transientPriorityRegistration, false)).ToList();
            foreach (var type in transientPriorityTypes)
            {
                services.AddTransient(type.Service, type.Implementation);
            }

            foreach (var type in types)
            {
                if (type.Implementation.IsDefined(scopedRegistration, false))
                {
                    services.AddScoped(type.Service, type.Implementation);
                }

                if (type.Implementation.IsDefined(transientRegistration, false))
                {
                    services.AddTransient(type.Service, type.Implementation);
                }

                if (type.Implementation.IsDefined(singletonRegistration, false))
                {
                    services.AddSingleton(type.Service, type.Implementation);
                }
            }
        }
    }
}
