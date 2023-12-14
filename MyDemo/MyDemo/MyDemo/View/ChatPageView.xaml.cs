using ChatApp.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace ChatApp.View
{
    /// <summary>
    /// Interaction logic for ChatPageView.xaml
    /// </summary>
    public partial class ChatPageView : Page
    {
        ChatPageViewModel viewModel;

        public ChatPageView(ChatPageViewModel viewModel)
        {
            this.viewModel = viewModel;
            DataContext = this.viewModel;
            InitializeComponent();
            itemsControl.ItemsSource = viewModel.Messages;
        }

        private void GoBack_button_click(object sender, RoutedEventArgs e)
        {
            viewModel.GoBack();
        }

        private void Shake_button_click(object sender, RoutedEventArgs e)
        {
            viewModel.SendShake();
        }
    }
}
