using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Internal;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MDIX_ClearButtonCommand
{

    public sealed class MultiSelectComboBox : ComboBox
    {
        static MultiSelectComboBox()
        {
            ClearText.HandlesClearCommandProperty.OverrideMetadata(typeof(MultiSelectComboBox), new FrameworkPropertyMetadata(OnHandlesClearCommandChanged));
            HintProxyFabric.RegisterBuilder(c => c is MultiSelectComboBox, c => new MultiSelectComboBoxHintProxy((MultiSelectComboBox)c));

            var buildersInfo = typeof(HintProxyFabric).GetField("Builders", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            var buildersInstance = buildersInfo.GetValue(null);
            // HintProxyBuilder is a private type, so its kinda hard to build this
            // var builders = buildersInstance as List<HintProxyBuilder> 
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ContentControl();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is UIElement;
        }

        private static void OnHandlesClearCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is MultiSelectComboBox element)
            {
                if ((bool)e.NewValue)
                {
                    RemoveClearTextCommand(element);
                    element.CommandBindings.Add(new CommandBinding(ClearText.ClearCommand, OnClearCommand));
                }
                else
                {
                    RemoveClearTextCommand(element);
                }
            }

            static void RemoveClearTextCommand(MultiSelectComboBox element)
            {
                for (int i = element.CommandBindings.Count - 1; i >= 0; i--)
                {
                    if (element.CommandBindings[i].Command == ClearText.ClearCommand)
                    {
                        element.CommandBindings.RemoveAt(i);
                    }
                }
            }

            static void OnClearCommand(object sender, ExecutedRoutedEventArgs e)
            {
                if (sender is MultiSelectComboBox element && element.DataContext is MultiSelectionViewModel viewModel)
                {
                    viewModel.DeSelectAllCommand.Execute(null);
                    e.Handled = true;
                }
            }
        }
    }
}
