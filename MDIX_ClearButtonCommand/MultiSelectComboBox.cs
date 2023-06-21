using System.Windows;
using System.Windows.Controls;

namespace MDIX_ClearButtonCommand
{
    public sealed class MultiSelectComboBox : ComboBox
    {
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ContentControl();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is UIElement;
        }
    }
}
