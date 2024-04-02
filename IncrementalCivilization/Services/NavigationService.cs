using IncrementalCivilization.ViewModels;
using IncrementalCivilization.ViewModels.Pages;
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

    public void NavigateToMain()
    {
        NavigateTo<IMainViewModel>();
    }

    public void NavigateToSettings()
    {
        NavigateTo<ISettingsViewModel>();
    }

    public void NavigateTo<IVM>()
    {
        _logger.LogInformation("Navigating to {vm}", typeof(IVM).Name);

        var shell = _services.GetRequiredService<IShellViewModel>();
        var vm = _services.GetService<IVM>() as IViewModel;

        if (vm != null && !vm.Initialized)
            vm.Initialize();

        shell.Current = vm;
    }

    public void NavigateToPage(IPageViewModel page)
    {
        _logger.LogInformation("Navigating to page {page}", page.Title);

        if (!page.Initialized)
            page.Initialize();

        var mainVM = _services.GetRequiredService<IMainViewModel>();
        mainVM.CurrentPage = page;
    }
}
