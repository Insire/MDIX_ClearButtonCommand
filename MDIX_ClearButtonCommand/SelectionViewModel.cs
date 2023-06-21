using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging.Messages;
using CommunityToolkit.Mvvm.Messaging;

namespace MDIX_ClearButtonCommand
{
    public sealed partial class SelectionViewModel : ObservableObject
    {
        private readonly IMessenger _messenger;
        public string DisplayName { get; }

        [ObservableProperty]
        private bool _isSelected;

        public SelectionViewModel(string displayName, bool isSelected, IMessenger messenger)
        {
            DisplayName = displayName;
            IsSelected = isSelected;
            _messenger = messenger;

            _messenger.Register<PropertyChangedMessage<bool>>(this, (recipient, message) =>
            {
                if (ReferenceEquals(recipient, this))
                {
                    return;
                }

                IsSelected = false;
            });
        }

        partial void OnIsSelectedChanged(bool oldValue, bool newValue)
        {
            _messenger.Send(new PropertyChangedMessage<bool>(this, nameof(IsSelected), oldValue, newValue));
        }
    }
}
