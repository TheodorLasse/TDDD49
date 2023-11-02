using ChatApp.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace ChatApp.View
{
    /// <summary>
    /// Interaction logic for ConnectPageView.xaml
    /// </summary>
    public partial class ConnectPageView : Page
    {
        private ConnectPageViewModel viewModel;

        public ConnectPageView(ConnectPageViewModel viewModel)
        {
            this.viewModel = viewModel;
            DataContext = viewModel;
            InitializeComponent();
        }

        private void ConnectServer_button_click(object sender, RoutedEventArgs e)
        {
            viewModel.ConnectServer(ip.Text, int.Parse(port.Text), username.Text);
        }
        private void StartServer_button_click(object sender, RoutedEventArgs e)
        {
            viewModel.StartServer(ip.Text, int.Parse(port.Text), username.Text);
        }
    }
}
