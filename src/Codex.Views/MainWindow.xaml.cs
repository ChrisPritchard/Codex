using System;
using System.Windows;

namespace Codex.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SwitchTheme_Click(object sender, RoutedEventArgs e)
        {
            var theme = "Dark";
            Resources.MergedDictionaries[0].Source = new Uri($"Themes/{theme}.xaml", UriKind.Relative);
        }
    }
}
