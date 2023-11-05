using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using ChatApp.Model;
using ChatApp.View;
using ChatApp.ViewModel;

namespace ChatApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Frame MainFrame;
        public static Window Window;
        public MainWindow()
        {
            InitializeComponent();
            MainFrame = mainFrame;
            Window = this;
            MainFrame.NavigationService.Navigate(new ConnectPageView(new ViewModel.ConnectPageViewModel(new ProtocolManager(), new ChatHistory())));
        }

        public static void Shake()
        {
            var storyboard = (Storyboard)Window.FindResource("shakeKey");
            storyboard.Begin(Window);
        }
    }
}
