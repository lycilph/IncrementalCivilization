namespace IncrementalCivilization.Mvvm.Services
{
    public interface INavigationService
    {
        void Initialize();
        void NavigateTo<IVM>();
    }
}