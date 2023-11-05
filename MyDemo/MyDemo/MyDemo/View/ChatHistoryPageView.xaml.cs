using ChatApp.Model;
using ChatApp.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ChatApp.View
{
    /// <summary>
    /// Interaction logic for ChatHistoryPageView.xaml
    /// </summary>
    public partial class ChatHistoryPageView : Page
    {
        ChatHistoryPageViewModel viewModel;
        public ChatHistoryPageView(ChatHistoryPageViewModel viewModel)
        {
            this.viewModel = viewModel;
            DataContext = viewModel;
            InitializeComponent();
        }

        private void GoBack_button_click(object sender, RoutedEventArgs e)
        {
            viewModel.GoBack();
        }
    }
}
