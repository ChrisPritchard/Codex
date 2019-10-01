using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Codex
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainText.Focus();
        }

        private void Exit_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        private void MainText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (WordCount == null)
                return;
            var range = new TextRange(MainText.Document.ContentStart, MainText.Document.ContentEnd);
            var wordCount = range.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
            WordCount.Text = $"{wordCount} words";
        }
    }
}
