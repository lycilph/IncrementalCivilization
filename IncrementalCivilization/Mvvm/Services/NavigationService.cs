using IncrementalCivilization.Mvvm.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace IncrementalCivilization.Mvvm.Services;

public class NavigationService : INavigationService
{
    private IServiceProvider _services;

    public NavigationService(IServiceProvider services)
    {
        _services = services;
    }

    public void NavigateTo<IVM>()
    {
        Trace.WriteLine($"Navigation Service - Navigating to {typeof(IVM).Name}");

        var shell = _services.GetRequiredService<IShellViewModel>();
        var vm = _services.GetService<IVM>();

        shell.Current = vm as IViewModel;
    }

    public void Initialize()
    {
        Trace.WriteLine("Navigation Service - Initialize");

        NavigateTo<IMainViewModel>();
    }
}
