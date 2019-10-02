using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;

namespace Codex
{
    public partial class MainWindow : Window
    {
        private const string fileFilters = "RTF (*.rtf)|*.rtf|Plain Text (*.txt)|*.txt|XAML Pack (*.xaml)|*.xaml";
        private bool textDirty = false;

        private string lastFilename = null;
        private readonly DispatcherTimer autosaveTimer;

        public MainWindow()
        {
            InitializeComponent();
            MainText.Focus();

            Closing += MainWindow_Closing;
            autosaveTimer = new DispatcherTimer { Interval = TimeSpan.FromMinutes(0.1) };
            autosaveTimer.Tick += (o, e) => Save();
            autosaveTimer.Start();
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!textDirty)
                return;

            var result = MessageBox.Show("Text has been changed. Would you like to save?", "Text has changed", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Cancel)
                e.Cancel = true;
            else if (result == MessageBoxResult.Yes)
                Save_Click(null, null);
        }

        private string DataFormatForExtension(string extension) =>
            extension switch
            {
                ".txt" => DataFormats.Text,
                ".rtf" => DataFormats.Rtf,
                _ => DataFormats.Xaml,
            };

        private void Save()
        {
            if (lastFilename == null || !textDirty)
                return;

            Saving.Text = "saving...";

            var type = DataFormatForExtension(Path.GetExtension(lastFilename));
            var range = new TextRange(MainText.Document.ContentStart, MainText.Document.ContentEnd);
            using var stream = File.Create(lastFilename);
            range.Save(stream, type);

            Action toInvoke = () => Saving.Text = "";
            Dispatcher.Invoke(toInvoke, DispatcherPriority.Normal, CancellationToken.None, TimeSpan.FromSeconds(3));
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (lastFilename == null)
            {
                var dialog = new SaveFileDialog { Filter = fileFilters };
                if (dialog.ShowDialog() != true)
                    return;
                lastFilename = dialog.FileName;
            }

            Save();
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

            textDirty = false;
            lastFilename = fileName;
        }

        private void Exit_Click(object sender, RoutedEventArgs e) => Close();

        private void MainText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (WordCount == null)
                return;
            var range = new TextRange(MainText.Document.ContentStart, MainText.Document.ContentEnd);
            var wordCount = range.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
            WordCount.Text = $"{wordCount} words";

            textDirty = true;
        }
    }
}
