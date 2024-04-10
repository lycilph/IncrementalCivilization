using IncrementalCivilization.ViewModels.Shared;
using IncrementalCivilization.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace IncrementalCivilization.Services;

public class NavigationService : INavigationService
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    private readonly IServiceProvider _services;

    public NavigationService(IServiceProvider services)
    {
        _services = services;
    }

    public void NavigateToScreen<IVM>() where IVM : notnull
    {
        logger.Debug("Navigating to {vm}", typeof(IVM).Name);

        var shell = _services.GetRequiredService<IShellViewModel>();
        var vm = _services.GetRequiredService<IVM>() as IViewModel;

        if (vm != null && !vm.Initialized)
            vm.Initialize();

        shell.CurrentScreen = vm;
    }
}
