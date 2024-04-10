using IncrementalCivilization.ViewModels;
using IncrementalCivilization.ViewModels.Screens;
using IncrementalCivilization.ViewModels.Shared;
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
        logger.Debug("Navigating to screen {vm}", typeof(IVM).Name);

        var shell = _services.GetRequiredService<IShellViewModel>();
        var vm = _services.GetRequiredService<IVM>() as IViewModel;

        if (vm != null && !vm.Initialized)
            vm.Initialize();

        shell.CurrentScreen = vm;
    }

    public void NavigateToPage(IPageViewModel page)
    {
        logger.Debug("Navigating to page {page}", page.Title);

        if (!page.Initialized)
            page.Initialize();

        var mainVM = _services.GetRequiredService<IMainScreenViewModel>();
        mainVM.CurrentPage = page;
    }
}
