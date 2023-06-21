using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace MDIX_ClearButtonCommand
{
    public sealed partial class CommandWrapperViewModel : ObservableObject
    {
        public ICommand Command { get; }

        public string DisplayName { get; }

        public CommandWrapperViewModel(ICommand command, string displayName)
        {
            Command = command;
            DisplayName = displayName;
        }
    }
}
