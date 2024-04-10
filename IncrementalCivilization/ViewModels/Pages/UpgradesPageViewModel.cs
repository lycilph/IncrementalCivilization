using CommunityToolkit.Mvvm.Messaging;
using IncrementalCivilization.Messages;
using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels.Shared;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Pages;

public class UpgradesPageViewModel : PageViewModelBase, IRecipient<EnablePageMessage>
{
    public UpgradesPageViewModel(INavigationService navigationService) : base(navigationService, "Upgrades", SymbolRegular.Star24)
    {
        StrongReferenceMessenger.Default.Register(this);
    }

    public void Receive(EnablePageMessage message)
    {
        if (message.PageToEnable == EnablePageMessage.Page.All || message.PageToEnable == EnablePageMessage.Page.Upgrades)
            Enabled = true;
    }
}
