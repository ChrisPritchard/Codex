using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Codex.Views
{
    public partial class BindableRichTextBox : UserControl, INotifyPropertyChanged
    {
        public BindableRichTextBox()
        {
            InitializeComponent();
            CoreRichTextBox.Focus();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private static TextRange GetTextRange(RichTextBox richTextBox) =>
            new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);

        public string XamlContent
        {
            get => (string)GetValue(XamlContentProperty);
            set => SetValue(XamlContentProperty, value);
        }

        private static readonly DependencyProperty XamlContentProperty =
            DependencyProperty.Register("XamlContent", typeof(string), typeof(BindableRichTextBox), 
                new PropertyMetadata(OnXamlContentChanged));

        private static void OnXamlContentChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue || string.IsNullOrWhiteSpace(e.NewValue as string))
                return;

            var richTextBox = (sender as BindableRichTextBox).CoreRichTextBox;
            var textRange = GetTextRange(richTextBox);

            using var readStream = new MemoryStream();
            textRange.Save(readStream, DataFormats.Xaml);
            var currentXaml = Encoding.UTF8.GetString(readStream.ToArray());

            if (currentXaml == (string)e.NewValue)
                return;

            using var writeStream = new MemoryStream(Encoding.UTF8.GetBytes((string)e.NewValue));
            textRange.Load(writeStream, DataFormats.Xaml);
        }

        public int WordCount { get; private set; }

        public bool TextDirty { get; private set; }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            var textRange = GetTextRange(CoreRichTextBox);

            WordCount = textRange.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
            TextDirty = true;

            using var stream = new MemoryStream();
            textRange.Save(stream, DataFormats.Xaml);
            XamlContent = Encoding.UTF8.GetString(stream.ToArray());

            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(nameof(XamlContent)));
            //PropertyChanged(this, new PropertyChangedEventArgs(nameof(WordCount)));
            //PropertyChanged(this, new PropertyChangedEventArgs(nameof(TextDirty)));
        }
    }}