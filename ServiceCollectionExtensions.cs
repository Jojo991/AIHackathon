using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IMvcBuilder AddCustomRazorRuntimeCompilation(this IMvcBuilder builder)
    {
        return builder;
    }
}