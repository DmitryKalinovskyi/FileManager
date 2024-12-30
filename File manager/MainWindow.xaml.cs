using File_manager.FileManager.Attributes;
using File_manager.FileManager.View;
using File_manager.FileManager.ViewModel;
using File_manager.FileManager.ViewModel.ListView;
using File_manager.FileManager.ViewModel.TreeView;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
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
            var selectedItem = (ListItemViewModel)selectedRow.Item;
            selectedItem.Open();
        }

        #region FileTreeView
        private void TreeView_Expanded(object sender, RoutedEventArgs e)
        {
            var item = e.OriginalSource as TreeViewItem;

            if (item != null && item.DataContext is ILazyLoader lazyLoader)
                lazyLoader.Load();
        }

        private void TreeView_Collapsed(object sender, RoutedEventArgs e)
        {
            var item = e.OriginalSource as TreeViewItem;

            if(item != null && item.DataContext is ILazyLoader lazyLoader)
                lazyLoader.Unload();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if(e.NewValue is ISelectableTreeItem item)
            {
                item.Select();
            }
        }

        #endregion

        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedRow = (ListViewItem)sender;
            var selectedItem = (ListItemViewModel)selectedRow.DataContext;
            selectedItem.Open();
        }

        #region ListView drag and drop


        private void ListBoxItem_Drop(object sender, DragEventArgs e)
        {
            if (sender is FrameworkElement frameworkElement && frameworkElement.DataContext is ListItemViewModel viewModel)
            {
                try
                {

                    object DropDataContext = e.Data.GetData(DataFormats.FileDrop);

                    if (DropDataContext != null && DropDataContext is string[] paths)
                    {
                        if (FileDropValidation(paths, viewModel.FullName))
                        {
                            //Trace.WriteLine("Dropped");

                            // put items inside folder..

                            Trace.WriteLine($"Move [{paths[0]}] into {viewModel.FullName}");

                            object arg = new object[] { paths, viewModel.FullName };
                            ((FileManagerViewModel)DataContext).DropFilesinDirectoryCommand.Execute(arg);
                            //((FileManagerViewModel)DataContext).
                        }
                    }
                }
                catch
                {
                    Trace.WriteLine("Incorrect format of dataDrop");
                }

            }

            e.Handled = true;
        }

        private void ListBoxItem_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && sender is ListViewItem item)
            {
                if(item.DataContext is ListItemViewModel viewModel)
                {
                    DragDrop.DoDragDrop(item,
                    new DataObject(DataFormats.FileDrop, new string[] {viewModel.FullName}),
                    DragDropEffects.Move);
                }
            }
        }

        private bool FileDropValidation(string[] data, string destinationSource)
        {
            // We can't put folder inside self
            return !data.Contains(destinationSource);
        }

        private void ListBoxItem_DragEnter(object sender, DragEventArgs e)
        {
            if (sender is FrameworkElement frameworkElement && frameworkElement.DataContext is ListItemViewModel viewModel)
            {
                object DropDataContext = e.Data.GetData(DataFormats.FileDrop);

                if (DropDataContext != null && DropDataContext is string[] paths)
                {
                    if (FileDropValidation(paths, viewModel.FullName))
                    {
                        // display possibility of drop
                    }
                }
            }

            e.Handled = true;
        }

        private void ListView_Drop(object sender, DragEventArgs e)
        {
            object DropDataContext = e.Data.GetData(DataFormats.FileDrop);

            string[] paths = (string[])DropDataContext;

            ((FileManagerViewModel)DataContext).DropFilesCommand.Execute(paths);
        }

        private void ListView_DragEnter(object sender, DragEventArgs e)
        {
            Trace.WriteLine("Trying to drop a data into listview");
        }

        private void FileListView_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && sender is ListView listView)
            {




            }
        }
        #endregion

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            InformationWindow information = new InformationWindow();
            information.Owner = this;
            information.Show();
        }
    }
}
