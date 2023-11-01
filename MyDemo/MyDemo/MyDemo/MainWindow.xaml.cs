using System.Windows;
using System.Windows.Controls;
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
            MainFrame = this.mainFrame;
            MainFrame.NavigationService.Navigate(new LoginPageView());
        }
    }
}
