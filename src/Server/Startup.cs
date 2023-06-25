using FBTracker.Server.Data.Services;
using FBTracker.Server.Data;

namespace FBTracker.Server;

internal static class Startup
{
    internal static void AddLogging(
        WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        builder.Logging.AddEventSourceLogger();
    }

    internal static void AddControllersAndViews(
        IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddRazorPages();
    }

    internal static void AddDomainServices(
        IServiceCollection services)
    {
        services.AddSingleton<DataContext>();
        services.AddTransient<StateService>();
        services.AddTransient<ScheduledGamesService>();
        services.AddTransient<TeamsService>();
    }
}
