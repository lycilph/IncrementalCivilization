using CommunityToolkit.Mvvm.Messaging;
using IncrementalCivilization.Messages;
using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels.Shared;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Pages;

public class TimePageViewModel : PageViewModelBase, IRecipient<EnablePageMessage>
{
    public TimePageViewModel(INavigationService navigationService) : base(navigationService, "Time", SymbolRegular.HourglassHalf24)
    {
        StrongReferenceMessenger.Default.Register(this);
    }

    public void Receive(EnablePageMessage message)
    {
        if (message.PageToEnable == EnablePageMessage.Page.All || message.PageToEnable == EnablePageMessage.Page.Upgrades)
            Enabled = true;
    }
}
