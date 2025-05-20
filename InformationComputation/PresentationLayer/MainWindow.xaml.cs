using LogicLayer;
using Microsoft.Win32;
using PresentationLayer;
using System.Windows;

namespace PresentationLayer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Get logic API from factory
            LogicAPI logic = LogicFactory.Create();

            // Pass logic into ViewModel
            UserViewModel userViewModel = new UserViewModel(logic);

            // Set DataContext to ViewModel
            this.DataContext = userViewModel;
        }
    }
}