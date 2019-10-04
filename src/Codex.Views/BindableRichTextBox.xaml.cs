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

        public FlowDocument Document
        {
            get { return (FlowDocument)GetValue(DocumentProperty); }
            set { SetValue(DocumentProperty, value); }
        }

        public static readonly DependencyProperty DocumentProperty =
            DependencyProperty.Register("Document", 
                typeof(FlowDocument), typeof(BindableRichTextBox), new PropertyMetadata(OnDocumentChanged));

        private static void OnDocumentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (BindableRichTextBox)d;
            if (e.NewValue == null)
                control.CoreRichTextBox.Document = new FlowDocument(); // Document is not amused by null :)
            else
                control.CoreRichTextBox.Document = (FlowDocument)e.NewValue;
        }
    }
}
