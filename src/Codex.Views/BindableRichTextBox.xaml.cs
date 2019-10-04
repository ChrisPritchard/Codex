using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Codex.Views
{
    public partial class BindableRichTextBox : UserControl
    {
        public BindableRichTextBox()
        {
            InitializeComponent();
            CoreRichTextBox.Focus();
        }

        private static readonly DependencyProperty WordCountProperty =
            DependencyProperty.Register(nameof(WordCount), typeof(int), typeof(BindableRichTextBox));
        private static readonly DependencyProperty IsDirtyProperty =
            DependencyProperty.Register(nameof(IsDirty), typeof(bool), typeof(BindableRichTextBox));
        private static readonly DependencyProperty XamlContentProperty =
            DependencyProperty.Register(nameof(XamlContent), typeof(string), typeof(BindableRichTextBox));

        public int WordCount { get => (int)GetValue(WordCountProperty); private set => SetValue(WordCountProperty, value); }
        public bool IsDirty { get => (bool)GetValue(IsDirtyProperty); private set => SetValue(IsDirtyProperty, value); }
        public string XamlContent { get => (string)GetValue(XamlContentProperty); set => SetValue(XamlContentProperty, value); }

        private TextRange CurrentRange => new TextRange(CoreRichTextBox.Document.ContentStart, CoreRichTextBox.Document.ContentEnd);

        private string Xaml
        {
            get
            {
                using var stream = new MemoryStream();
                CurrentRange.Save(stream, DataFormats.Xaml);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
            set
            {
                using var stream = new MemoryStream(Encoding.UTF8.GetBytes(value));
                CurrentRange.Load(stream, DataFormats.Xaml);
            }
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            WordCount = CurrentRange.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
            IsDirty = true;
            XamlContent = Xaml;
        }
    }
}
 