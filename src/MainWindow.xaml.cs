using Microsoft.Win32;
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

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog { Filter = "RTF (*.rtf)|*.rtf|Plain Text (*.txt)|*.txt|XAML Pack (*.xaml)|*.xaml" };
            if (dialog.ShowDialog() != true)
                return;

            var fileName = dialog.FileName;
        }

        private void Load_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

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
