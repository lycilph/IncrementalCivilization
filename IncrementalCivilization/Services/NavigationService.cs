using IncrementalCivilization.ViewModels;
using IncrementalCivilization.ViewModels.Core;
using IncrementalCivilization.ViewModels.Screens;
using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace IncrementalCivilization.Services;

public class NavigationService(IServiceProvider services) : INavigationService
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    public void NavigateToScreen<IVM>() where IVM : notnull
    {
        logger.Debug("Navigating to screen {vm}", typeof(IVM).Name);

        var shell = services.GetRequiredService<IShellViewModel>();

        if (services.GetRequiredService<IVM>() is not IViewModel vm)
            throw new InvalidOperationException($"Could not navigate to {typeof(IVM).Name}");

        shell.CurrentScreen?.Deactivate();
        shell.CurrentScreen = vm;
        shell.CurrentScreen?.Activate();
    }

    public void NavigateToPage<IVM>() where IVM : IPageViewModel
    {
        logger.Debug("Navigating to page {vm}", typeof(IVM).Name);

        var mainVM = services.GetRequiredService<IMainScreenViewModel>();

        if (services.GetRequiredService<IVM>() is not IPageViewModel vm)
            throw new InvalidOperationException($"Could not navigate to {typeof(IVM).Name}");

        mainVM.CurrentPage?.Deactivate();
        mainVM.CurrentPage = vm;
        mainVM.CurrentPage?.Activate();
    }

    public void NavigateToPage(IPageViewModel page)
    {
        logger.Debug("Navigating to page {page}", page.Title);

        var mainVM = services.GetRequiredService<IMainScreenViewModel>();

        mainVM.CurrentPage?.Deactivate();
        mainVM.CurrentPage = page;
        mainVM.CurrentPage?.Activate();
    }
}
