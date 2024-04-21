using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
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
        _ = HandleLoadedAsync().ConfigureAwait(false);
    }

    protected virtual void HandleLoaded() { }

    protected virtual Task HandleLoadedAsync()
    {
        return Task.CompletedTask;
    }

    public void OnClosed()
    {
        Messenger.UnregisterAll(this);

        HandleClosed();
        _ = HandleClosedAsync().ConfigureAwait(false);
    }

    protected virtual void HandleClosed() { }

    protected virtual Task HandleClosedAsync()
    {
        return Task.CompletedTask;
    }
}
