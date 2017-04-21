using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace asstool.Model
{
    class TextBoxCursorIndex
    {
        public static readonly DependencyProperty CursorIndexProperty = DependencyProperty.RegisterAttached(
            "CursorIndex",
            typeof(int),
            typeof(TextBoxCursorIndex),
            new FrameworkPropertyMetadata(OnCursorIndexChanged));

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        static void RegistryCursorIndex(TextBox tb)
        {
            tb.TextInput += tb_TextInput;
        }

        static void tb_TextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            return;
        }
        
        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static int GetCursorIndex(TextBox tb)
        {
            return (int)tb.GetValue(CursorIndexProperty);
        }

        public static void SetCursorIndex(TextBox tb,int value)
        {
            tb.SetValue(CursorIndexProperty, value);
        }


        static void OnCursorIndexChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            TextBox tb = dependencyObject as TextBox;
            if (tb == null && e.NewValue != null)
                tb.SelectionStart = (int)e.NewValue;
        }
    }
}
