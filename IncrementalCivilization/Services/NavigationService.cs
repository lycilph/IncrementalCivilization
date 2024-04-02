using IncrementalCivilization.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IncrementalCivilization.Services;

public class NavigationService : INavigationService
{
    private readonly IServiceProvider _services;
    private readonly ILogger _logger;

    public NavigationService(IServiceProvider services, ILogger<NavigationService> logger)
    {
        _services = services;
        _logger = logger;
    }

    public void NavigateTo<IVM>()
    {
        _logger.LogInformation("Navigating to {vm}", typeof(IVM).Name);

        var shell = _services.GetRequiredService<IShellViewModel>();
        var vm = _services.GetService<IVM>();

        shell.Current = vm as IViewModel;
    }
}
