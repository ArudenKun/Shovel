using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using HanumanInstitute.MvvmDialogs;

namespace Shovel.ViewModels.Abstractions;

[ObservableRecipient]
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public abstract partial class ViewModelBase : ObservableValidator, IViewLoaded, IViewClosed
{
    protected IDialogService DialogService { get; }

    protected ViewModelBase(IDialogService dialogService)
    {
        DialogService = dialogService;

        Messenger = StrongReferenceMessenger.Default;
    }

    [UnconditionalSuppressMessage(
        "Trimming",
        "IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code",
        Justification = "<Pending>"
    )]
    public void OnLoaded()
    {
        Messenger.RegisterAll(this);
        HandleLoaded();
    }

    protected virtual void HandleLoaded() { }

    [UnconditionalSuppressMessage(
        "Trimming",
        "IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code",
        Justification = "<Pending>"
    )]
    public void OnClosed()
    {
        Messenger.UnregisterAll(this);
        HandleOnClosed();
    }

    protected virtual void HandleOnClosed() { }
}
