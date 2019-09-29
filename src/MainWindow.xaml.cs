using System.Windows;
using System.Windows.Controls;

namespace Codex
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            richTextBox.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
