using File_manager.FileManager.Attributes;
using File_manager.FileManager.ViewModel;
using System;
using System.Collections.Generic;
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

        //private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        //{
        //    var propertyName = e.PropertyName;

        //    var dataContext = (sender as FrameworkElement).DataContext;
        //    var prop = dataContext.GetType().GetProperty(propertyName);



        //    // Перевіряємо, чи має властивість атрибут DataGridDisplayAttribute
        //    if (Attribute.IsDefined(prop, typeof(DataGridDisplayAttribute)))
        //    {
        //        // Створюємо новий стовпчик для властивості з атрибутом
        //        var column = new DataGridTextColumn();

        //        // Встановлюємо заголовок стовпчика як назву властивості
        //        column.Header = e.PropertyName;

        //        // Встановлюємо прив'язку значення властивості до стовпчика
        //        column.Binding = new Binding(e.PropertyName);

        //        // Додаємо стовпчик до DataGrid
        //        e.Column = column;
        //    }

        //}

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
                if (e.EditingEventArgs.Source is TextBox textBox)
                {
                    textBox.Focus();
                    textBox.SelectAll();
                }
            }), System.Windows.Threading.DispatcherPriority.Background);
        }
    }
}
