using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PresentationLayer.ViewModels;
using DataLayer;
using LogicLayer;

namespace PresentationLayer;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        try
        {
            DatabaseInitializer.Initialize();
            
            InitializeComponent();
            
            // abstract / dependency injection
            ILogicService logicService = ServiceLocator.GetLogicService();
            DataContext = new MainViewModel(logicService);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error initializing application: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}