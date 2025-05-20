using DataLayer;
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

            // Create the data API
            Events events = new Events();

            // Create logic API
            LogicAPI logic = new Logic(events);

            // Pass logic into ViewModel
            UserViewModel userViewModel = new UserViewModel(logic);

            // Set DataContext to ViewModel
            this.DataContext = userViewModel;
        }
    }
}