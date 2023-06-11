using File_manager.FileManager.Attributes;
using File_manager.FileManager.ViewModel;
using File_manager.FileManager.ViewModel.ListView;
using File_manager.FileManager.ViewModel.TreeView;
using System;
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
            var selectedItem = (ListItemViewModel)selectedRow.Item;
            selectedItem.Row_MouseDoubleClick(sender, e);
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
            selectedItem.Row_MouseDoubleClick(sender, e);
        }

        private void ListBoxItem_Drop(object sender, DragEventArgs e)
        {
            if (sender is FrameworkElement frameworkElement && frameworkElement.DataContext is ListItemViewModel viewModel)
            {
                object DropDataContext = e.Data.GetData(DataFormats.FileDrop);

                if (DropDataContext != null && DropDataContext is string[] paths)
                {
                    if (ListBoxItem_DragEnterValid(paths, viewModel.FullName))
                    {
                        Trace.WriteLine("Dropped");
                    }
                    else
                    {
                        Trace.WriteLine("Drop failed!");
                    }

                }
            }

            e.Handled = true;
        }

        private void ListBoxItem_MouseMove(object sender, MouseEventArgs e)
        {
            // TODO: selected items drag simulation
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

        private bool ListBoxItem_DragEnterValid(string[] data, string source)
        {
            if (data.Contains(source))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void ListBoxItem_DragEnter(object sender, DragEventArgs e)
        {
            if (sender is FrameworkElement frameworkElement && frameworkElement.DataContext is ListItemViewModel viewModel)
            {
                object DropDataContext = e.Data.GetData(DataFormats.FileDrop);

                if (DropDataContext != null && DropDataContext is string[] paths)
                {
                    if (ListBoxItem_DragEnterValid(paths, viewModel.FullName))
                    {
                    }
                    else
                    {
                    }

                }
            }

            e.Handled = true;


            //Trace.WriteLine("Initial " + sender.GetType());

            //if (sender is FrameworkElement frameworkElement)
            //{
            //    object DropDataContext = e.Data.GetData(DataFormats.FileDrop);
            //    // is allowed?



            //    if (DropDataContext == frameworkElement.DataContext)
            //        return;

            //    if(DropDataContext is FileItemViewModel)
            //        Trace.WriteLine("Entered to other object");

            //    if(DropDataContext is string[] arr)
            //    {
            //        Trace.WriteLine("Dropped: ");
            //        foreach(string path in arr)
            //        {
            //            Trace.WriteLine(path);
            //        }
            //    }
            //}

            //e.Handled = true;

        }

        private void ListView_Drop(object sender, DragEventArgs e)
        {
            Trace.WriteLine("DataDropped inside ListView");
        }

        private void ListView_DragEnter(object sender, DragEventArgs e)
        {
            Trace.WriteLine("Trying to drop a data into listview");
        }


        private void ListView_PreviewDrop(object sender, DragEventArgs e)
        {
            if(sender is ListViewItem item && item.DataContext is ListItemViewModel viewModel) 
            {
                Trace.WriteLine("Moved into FIleItem");
            }
            else if(sender is ListView view && view.DataContext is FileListViewModel grid)
            {
                Trace.WriteLine("Moved into ListView");
            }

            e.Handled = true;
        }

        private void ListView_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && sender is ListView listView)
            {

                var selected = listView.SelectedItems;
                Trace.WriteLine("Selected: ");

                List<string> paths = new();
                foreach (var item in selected)
                {
                    paths.Add((item as ListItemViewModel).FullName);
                }

                

            }




        }

        
    }
}
