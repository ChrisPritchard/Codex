using Microsoft.Win32;
using System;
using System.IO;
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

        private const string fileFilters = "RTF (*.rtf)|*.rtf|Plain Text (*.txt)|*.txt|XAML Pack (*.xaml)|*.xaml";

        private string DataFormatForExtension(string extension) =>
            extension switch
            {
                ".txt" => DataFormats.Text,
                ".rtf" => DataFormats.Rtf,
                _ => DataFormats.Xaml,
            };

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog { Filter = fileFilters };
            if (dialog.ShowDialog() != true)
                return;

            var fileName = dialog.FileName;
            var type = DataFormatForExtension(Path.GetExtension(fileName));

            var range = new TextRange(MainText.Document.ContentStart, MainText.Document.ContentEnd);
            using var stream = File.Create(fileName);
            range.Save(stream, type);
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog { Filter = fileFilters };
            if (dialog.ShowDialog() != true)
                return;

            var fileName = dialog.FileName;
            var type = DataFormatForExtension(Path.GetExtension(fileName));

            var range = new TextRange(MainText.Document.ContentStart, MainText.Document.ContentEnd);
            using var stream = File.OpenRead(fileName);
            range.Load(stream, type);
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
