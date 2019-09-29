using System.Windows;
using System.Windows.Controls;

namespace Codex
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            richTextBox.Focus();
        }
    }
}
