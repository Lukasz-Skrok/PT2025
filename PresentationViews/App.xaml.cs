using System.Configuration;
using System.Data;
using System.Windows;
using PresentationViewModels;
using LogicLayer;
using System;

namespace PresentationViews
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            MainViewModel mainViewModel = null;
            MainWindow mainWindow = null;

            try
            {
                // Initialize the Logic Layer and get the LogicService
                var logicService = LogicLayerInitializer.InitializeLogicService();
                mainViewModel = new MainViewModel(logicService);
                
            }
            catch (Exception ex)
            {
                // Log the exception or show a message
                MessageBox.Show($"An error occurred during application initialization: {ex.Message}", "Initialization Error", MessageBoxButton.OK, MessageBoxImage.Error);
                // The mainViewModel will remain null
            }

            // Always attempt to show the main window
            mainWindow = new MainWindow(mainViewModel);
            mainWindow.Show();
        }
    }
}
