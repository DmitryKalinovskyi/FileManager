using File_manager.FileManager.Model;
using File_manager.FileManager.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace File_manager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public AppDataManager DataManager { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.DispatcherUnhandledException += App_DispatcherUnhandledException;

            string defaultSaveDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Artemyshin Manager";
            DataManager = new AppDataManager(defaultSaveDirectory);


            MainWindow = new MainWindow 
            {
                DataContext = new FileManager.ViewModel.FileManagerViewModel(e.Args)
            };

            MainWindow.Show();

            
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"An unhandled exception occurred: {e.Exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            e.Handled = true;
            Shutdown();
        }
    }
}
