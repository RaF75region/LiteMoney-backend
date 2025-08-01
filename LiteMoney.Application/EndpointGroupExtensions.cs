using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace LiteMoney.Application.Interfaces;

public static class EndpointGroupExtensions
{
    public static IServiceCollection AddEndpointGroups(this IServiceCollection services, params Assembly[] assemblies)
    {
        var types = assemblies.SelectMany(a => a.DefinedTypes)
            .Where(t => typeof(IEndpointGroup).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface);
        foreach (var type in types)
        {
            services.AddSingleton(typeof(IEndpointGroup), type);
        }
        return services;
    }

    public static void MapEndpointGroups(this WebApplication app)
    {
        var groups = app.Services.GetRequiredService<IEnumerable<IEndpointGroup>>();
        foreach (var group in groups)
        {
            group.Map(app);
        }
    }
}
