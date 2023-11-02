using System.Windows;
using System.Windows.Controls;
using ChatApp.Model;
using ChatApp.View;

namespace ChatApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Frame MainFrame;
        public MainWindow()
        {
            InitializeComponent();
            MainFrame = mainFrame;
            MainFrame.NavigationService.Navigate(new ConnectPageView(new ViewModel.ConnectPageViewModel(new ProtocolManager())));
        }
    }
}
