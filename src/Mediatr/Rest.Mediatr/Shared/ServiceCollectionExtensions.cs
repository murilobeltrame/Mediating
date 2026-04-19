// using System.Reflection;

// using Application.Shared;

// using Microsoft.Extensions.DependencyInjection.Extensions;

// namespace Rest.Mediatr.Shared;

// public static class ServiceCollectionExtensions
// {
//     public static IServiceCollection AddEnrichersFromAssembly(
//         this IServiceCollection services, 
//         Assembly assembly)
//     {
//         var openGenericType = typeof(IEnricher<>);
        
//         var query = from type in assembly.GetExportedTypes()
//             where !type.IsAbstract && !type.IsGenericTypeDefinition
//                 let interfaces = type.GetInterfaces()
//                 let genericInterfaces = interfaces.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == openGenericType)
//                 let matchingInterface = genericInterfaces.FirstOrDefault()
//                 where matchingInterface != null
//                 select (matchingInterface, type);

//         foreach ((Type? matchingInterface, Type? type) in query)
//         {
//             services.TryAddEnumerable(new ServiceDescriptor(
//                 serviceType: matchingInterface,
//                 implementationType: type,
//                 lifetime: ServiceLifetime.Scoped));
            
//             services.TryAdd(new ServiceDescriptor(
//                 serviceType: type,
//                 implementationType: type,
//                 lifetime: ServiceLifetime.Scoped));
//         }
        
//         return services;
//     }
// }