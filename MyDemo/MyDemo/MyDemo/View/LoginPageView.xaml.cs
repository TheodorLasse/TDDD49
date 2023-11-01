using ChatApp.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace ChatApp.View
{
    /// <summary>
    /// Interaction logic for LoginPageView.xaml
    /// </summary>
    public partial class LoginPageView : Page
    {
        private LoginPageViewModel viewModel;

        public LoginPageView(LoginPageViewModel viewModel)
        {
            this.viewModel = viewModel;
            DataContext = viewModel;
            InitializeComponent();
        }

        private void Login_button_click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrame.NavigationService.Navigate(new ChatPageView());
        }
        private void Listen_button_click(object sender, RoutedEventArgs e)
        {
            viewModel.StartListening(ip.Text, int.Parse(port.Text));
        }
    }
}
