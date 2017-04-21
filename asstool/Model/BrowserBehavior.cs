using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using CefSharp.Wpf;

namespace asstool.Model
{
    public class BrowserBehavior
    {
        public static readonly DependencyProperty HtmlProperty = DependencyProperty.RegisterAttached(
                "Html",
                typeof(string),
                typeof(BrowserBehavior),
                new FrameworkPropertyMetadata(OnHtmlChanged));

        [AttachedPropertyBrowsableForType(typeof(ChromiumWebBrowser))]
        public static string GetHtml(ChromiumWebBrowser d)
        {
            return (string)d.GetValue(HtmlProperty);
        }

        public static void SetHtml(ChromiumWebBrowser d, string value)
        {
            d.SetValue(HtmlProperty, value);
        }

        static void OnHtmlChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            ChromiumWebBrowser webBrowser = dependencyObject as ChromiumWebBrowser;
            if (webBrowser != null)
                webBrowser.LoadHtml(e.NewValue as string ?? "&nbsp;", "http://rendering/");

        }
    }
}
