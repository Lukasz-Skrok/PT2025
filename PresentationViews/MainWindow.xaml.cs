using System.Windows;
using PresentationViewModels;

namespace PresentationViews
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void ShowPopup_Click(object sender, RoutedEventArgs e)
        {
            var popup = new PopupWindow();
            popup.ShowDialog();
        }
    }
}