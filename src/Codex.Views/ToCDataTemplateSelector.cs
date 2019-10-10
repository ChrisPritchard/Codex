
using System.Windows;
using System.Windows.Controls;

namespace Codex.Views
{
    public class ToCDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (!(container is FrameworkElement element) || item == null || !(item is Model.Core.Part part))
                return null;

            var asGrouping = part.AsGrouping;
            if (asGrouping.Item1)
                return element.FindResource("GroupingTemplate") as DataTemplate;
            return element.FindResource("ContentTemplate") as DataTemplate;
        }
    }
}
