using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MDIX_ClearButtonCommand
{
    public sealed partial class MultiSelectionViewModel : ObservableObject
    {
        private readonly IMessenger _messenger;
        private readonly ObservableCollection<SelectionViewModel> _items;

        public ReadOnlyObservableCollection<CommandWrapperViewModel> Commands { get; }
        public ReadOnlyObservableCollection<SelectionViewModel> Items { get; }

        [ObservableProperty]
        private string? _displayString;

        public MultiSelectionViewModel()
        {
            _messenger = new WeakReferenceMessenger();
            _items = new ObservableCollection<SelectionViewModel>()
            {
                new SelectionViewModel("One", false, _messenger),
                new SelectionViewModel("Two", false, _messenger),
                new SelectionViewModel("Three", false, _messenger),
                new SelectionViewModel("Four", false, _messenger),
            };

            Items = new ReadOnlyObservableCollection<SelectionViewModel>(_items);
            Commands = new ReadOnlyObservableCollection<CommandWrapperViewModel>(new ObservableCollection<CommandWrapperViewModel>()
            {
                new CommandWrapperViewModel(SelectAllCommand,"Select All"),
                new CommandWrapperViewModel(DeSelectAllCommand,"Clear Selection"),
            });

            _messenger.Register<PropertyChangedMessage<bool>>(this, (recipient, message) =>
            {
                var count = Items.Where(p => p.IsSelected).Count();
                if (count == 0)
                {
                    DisplayString = null;
                    return;
                }

                if (count == 1)
                {
                    DisplayString = Items.Where(p => p.IsSelected).Single().DisplayName;
                    return;
                }

                if (count == Items.Count)
                {
                    DisplayString = "All selected";
                    return;
                }

                DisplayString = "Multiple selected";
            });
        }

        [RelayCommand]
        private void SelectAll()
        {
            foreach (var item in Items)
            {
                item.IsSelected = true;
            }
        }

        [RelayCommand]
        private void DeSelectAll()
        {
            foreach (var item in Items)
            {
                item.IsSelected = false;
            }
        }
    }
}
