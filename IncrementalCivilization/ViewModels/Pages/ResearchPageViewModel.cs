﻿using CommunityToolkit.Mvvm.Messaging;
using IncrementalCivilization.Messages;
using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels.Shared;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Pages;

public class ResearchPageViewModel : PageViewModelBase, IRecipient<EnablePageMessage>
{
    public ResearchPageViewModel(INavigationService navigationService) : base(navigationService, "Research", SymbolRegular.Beaker24)
    {
        StrongReferenceMessenger.Default.Register(this);
    }

    public void Receive(EnablePageMessage message)
    {
        if (message.PageToEnable == EnablePageMessage.Page.All || message.PageToEnable == EnablePageMessage.Page.Research)
            Enabled = true;
    }
}
