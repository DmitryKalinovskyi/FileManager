using File_manager.FileManager.ViewModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace File_manager.FileManager.View
{
    /// <summary>
    /// Interaction logic for SystemItemPropertiesWindow.xaml
    /// </summary>
    public partial class SystemItemPropertiesWindow : Window
    {
        public SystemItemPropertiesWindow()
        {
            InitializeComponent();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            FileManagerViewModel.Instance.RenameItem.Execute(new object[] { DataContext, textBox.Text });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox textBox = (TextBox)sender;

                FileManagerViewModel.Instance.RenameItem.Execute(new object[] { DataContext, textBox.Text });



                //BindingExpression bindingExpression = textBox.GetBindingExpression(TextBox.TextProperty);

                //if (bindingExpression != null)
                //{
                //    bindingExpression.UpdateSource();
                //}

                Keyboard.ClearFocus();
                e.Handled = true;
            }
        }
    }
}
