using File_manager.FileManager.Attributes;
using File_manager.FileManager.ViewModel;
using File_manager.FileManager.ViewModel.TreeView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace File_manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new FileManagerViewModel();
        }

        // Method to unfocus from TextBox by enter
        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                TextBox textBox = (TextBox)sender;
                BindingExpression bindingExpression = textBox.GetBindingExpression(TextBox.TextProperty);

                if (bindingExpression != null)
                {
                    bindingExpression.UpdateSource();
                }

                Keyboard.ClearFocus();
                e.Handled = true;
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
        }

        private void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                //need to find textbox
                var fe = sender as FrameworkElement;

                if(fe != null)
                {
                }
                //if (e.EditingEventArgs.Source is TextBox textBox)
                //{
                //    textBox.Focus();
                //    textBox.SelectAll();
                //}
            }), System.Windows.Threading.DispatcherPriority.Background);
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedRow = (DataGridRow)sender;
            var selectedItem = (FileItemViewModel)selectedRow.Item;
            selectedItem.Row_MouseDoubleClick(sender, e);
        }

        private void TreeView_Expanded(object sender, RoutedEventArgs e)
        {
            var item = e.OriginalSource as TreeViewItem;

            var fileItem = item.DataContext as FileItemTreeView;
            if (fileItem != null)
            {
                fileItem.UpdateItems();
            }

        }


        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

            var fileItem = e.NewValue as FileItemTreeView;

            if(fileItem != null)
            {
                fileItem.SelectItem();
            }
        }
    }
}
