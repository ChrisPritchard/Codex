﻿using Microsoft.Win32;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Codex.Views
{
    public partial class MainWindow : Window
    {
        private const string fileFilters = "RTF (*.rtf)|*.rtf|Plain Text (*.txt)|*.txt|XAML Pack (*.xaml)|*.xaml";
        //private bool textDirty = false;

        private string lastFilename = null;
        //private readonly DispatcherTimer autosaveTimer;

        public MainWindow()
        {
            InitializeComponent();
            MainText.Focus();

            //autosaveTimer = new DispatcherTimer { Interval = TimeSpan.FromMinutes(0.1) };
            //autosaveTimer.Tick += (o, e) => Save();
            //autosaveTimer.Start();
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            //if (!textDirty)
            //    return;

            //var result = MessageBox.Show("Text has been changed. Would you like to save?", "Text has changed", MessageBoxButton.YesNoCancel);
            //if (result == MessageBoxResult.Cancel)
            //    e.Cancel = true;
            //else if (result == MessageBoxResult.Yes)
            //    Save_Click(null, null);
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
            //if (lastFilename == null || !textDirty)
            //    return;

            //Saving.Text = "saving...";

            //try
            //{
            //    var type = DataFormatForExtension(Path.GetExtension(lastFilename));
            //    var range = new TextRange(MainText.Document.ContentStart, MainText.Document.ContentEnd);
            //    using var stream = File.Create(lastFilename);
            //    range.Save(stream, type);

            //    textDirty = false;

            //    Dispatcher.InvokeAsync(async () =>
            //    {
            //        await Task.Delay(1000);
            //        Saving.Text = "";
            //    });
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show("There was an error saving. Please try again.");
            //    File.WriteAllText($"saving-error-{Guid.NewGuid()}.log", ex.ToString());
            //}
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
            //var dialog = new OpenFileDialog { Filter = fileFilters };
            //if (dialog.ShowDialog() != true)
            //    return;

            //var fileName = dialog.FileName;

            //try
            //{
            //    var type = DataFormatForExtension(Path.GetExtension(fileName));

            //    var range = new TextRange(MainText.Document.ContentStart, MainText.Document.ContentEnd);
            //    using var stream = File.OpenRead(fileName);
            //    range.Load(stream, type);

            //    textDirty = false;
            //    lastFilename = fileName;
            //}
            //catch (ArgumentException ex) when (ex.Message.StartsWith("Unrecognized structure"))
            //{
            //    MessageBox.Show("Invalid file - it may be malformed or not in the correct format.");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("There was an error loading. Please try again.");
            //    File.WriteAllText($"loading-error-{Guid.NewGuid()}.log", ex.ToString());
            //}
        }

        private void Exit_Click(object sender, RoutedEventArgs e) => Close();

        private void MainText_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (WordCount == null)
            //    return;
            //var range = new TextRange(MainText.Document.ContentStart, MainText.Document.ContentEnd);
            //var wordCount = range.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
            //WordCount.Text = $"{wordCount} words";

            //textDirty = true;
        }
    }
}
