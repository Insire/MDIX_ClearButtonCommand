using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MDIX_ClearButtonCommand
{
    // wip
    public sealed class MultiSelectComboBoxHintProxy : IHintProxy
    {
        private readonly MultiSelectComboBox _multiSelectComboBox;

        public bool IsLoaded => _multiSelectComboBox.IsLoaded;

        public bool IsVisible => _multiSelectComboBox.IsVisible;

        public event EventHandler? ContentChanged;
        public event EventHandler? IsVisibleChanged;
        public event EventHandler? Loaded;
        public event EventHandler? FocusedChanged;

        public MultiSelectComboBoxHintProxy(MultiSelectComboBox multiSelectComboBox)
        {
            _multiSelectComboBox = multiSelectComboBox;
            _multiSelectComboBox.Loaded += ComboBoxLoaded;
            _multiSelectComboBox.IsVisibleChanged += ComboBoxIsVisibleChanged;
            _multiSelectComboBox.IsKeyboardFocusWithinChanged += ComboBoxIsKeyboardFocusWithinChanged;
        }
        private void ComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
            => _multiSelectComboBox.Dispatcher.InvokeAsync(() => ContentChanged?.Invoke(sender, EventArgs.Empty));
        private void ComboBoxIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
            => IsVisibleChanged?.Invoke(sender, EventArgs.Empty);
        private void ComboBoxLoaded(object sender, RoutedEventArgs e)
            => Loaded?.Invoke(sender, EventArgs.Empty);

        private void ComboBoxIsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
            => FocusedChanged?.Invoke(sender, EventArgs.Empty);

        public void Dispose()
        {
            _multiSelectComboBox.Loaded -= ComboBoxLoaded;
            _multiSelectComboBox.IsVisibleChanged -= ComboBoxIsVisibleChanged;
            _multiSelectComboBox.IsKeyboardFocusWithinChanged -= ComboBoxIsKeyboardFocusWithinChanged;
        }

        public bool IsEmpty()
        {
            return false;
        }

        public bool IsFocused()
        {
            return false;
        }
    }
}
