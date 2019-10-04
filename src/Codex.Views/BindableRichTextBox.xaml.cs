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

        private TextRange CurrentRange => new TextRange(CoreRichTextBox.Document.ContentStart, CoreRichTextBox.Document.ContentEnd);

        private static readonly DependencyProperty XamlContentProperty =
            DependencyProperty.Register("XamlContent", typeof(string), typeof(BindableRichTextBox));

        public string XamlContent
        {
            get => (string)GetValue(XamlContentProperty);
            set => SetValue(XamlContentProperty, value);
        }
            //get 
            //{
            //    using var stream = new MemoryStream();
            //    CurrentRange.Save(stream, DataFormats.Xaml);
            //    return Encoding.UTF8.GetString(stream.ToArray());
            //}
            //set 
            //{
            //    using var stream = new MemoryStream(Encoding.UTF8.GetBytes(value));
            //    CurrentRange.Load(stream, DataFormats.Xaml);
            //    TextDirty = false;
            //}

        public int WordCount { get; private set; }

        public bool TextDirty { get; private set; }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            WordCount = CurrentRange.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
            TextDirty = true;

            using var stream = new MemoryStream();
            CurrentRange.Save(stream, DataFormats.Xaml);
            XamlContent = Encoding.UTF8.GetString(stream.ToArray());

            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(nameof(XamlContent)));
            //PropertyChanged(this, new PropertyChangedEventArgs(nameof(WordCount)));
            //PropertyChanged(this, new PropertyChangedEventArgs(nameof(TextDirty)));
        }
    }
}
 