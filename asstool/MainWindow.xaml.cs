using asstool.ViewModel;
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using NHotkey;
using NHotkey.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Setting = asstool.Properties.Settings;

namespace asstool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            this.DataContext = MainViewModel.Instance;

            try
            {
                InitializeComponent();

                set_theme_and_accent();
            }
            catch(Exception)
            {

            }
            //MainViewModel.Instance.AssCmdVM.Show = Visibility.Visible;
            //MainViewModel.Instance.AssCmdVM.Input = "b";

        }


        void set_theme_and_accent()
        {
            // set accent 
            var accent = ThemeManager.GetAccent(Setting.Default.color);
            var theme = ThemeManager.GetAppTheme(Setting.Default.theme);
            if(accent != null && theme != null)
            {
                ThemeManager.ChangeAppStyle(Application.Current, accent, theme);
            }

        }


        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            MainViewModel.Instance.OrgIndex = (sender as TextBox).SelectionStart;

            if (e.Key == Key.Back && MainViewModel.Instance.AssCmdVM.Input != "")
                MainViewModel.Instance.AssCmdVM.Show = Visibility.Visible;
        }

        private void MetroWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MainViewModel.Instance.AssCmdVM.Show = Visibility.Hidden;

            MainViewModel.Instance.OrgIndex--;
        }


        #region hotkey

        public void OnUP(object sender, HotkeyEventArgs e)
        {
            if (MainViewModel.Instance.AssCmdVM.Show == Visibility.Visible)
            {
                MainViewModel.Instance.AssCmdVM.Index--;
            }

            e.Handled = true;

        }

        public void OnDown(object sender, HotkeyEventArgs e)
        {
            if (MainViewModel.Instance.AssCmdVM.Show == Visibility.Visible)
            {
                MainViewModel.Instance.AssCmdVM.Index++;
            }
            e.Handled = true;

        }
        public void OnSelect(object sender, HotkeyEventArgs e)
        {
            if (MainViewModel.Instance.AssCmdVM.Show == Visibility.Visible)
            {

                string org = MainViewModel.Instance.OrgAssCode;
                string head = org.Substring(0, input.SelectionStart - MainViewModel.Instance.AssCmdVM.Input.Length);
                string end = org.Substring(input.SelectionStart);
                string i = MainViewModel.Instance.AssCmdVM.Cmds[MainViewModel.Instance.AssCmdVM.Index];


                int org_start = input.SelectionStart;
                int org_input_len = MainViewModel.Instance.AssCmdVM.Input.Length;
                MainViewModel.Instance.OrgAssCode = head + i + end;
                input.SelectionStart = org_start + i.Length - org_input_len;

                MainViewModel.Instance.AssCmdVM.Show = Visibility.Hidden;
            }

            e.Handled = true;
        }

        void registry()
        {
            try
            {
                HotkeyManager.Current.AddOrReplace("Up", Key.Up, ModifierKeys.None, OnUP);
                HotkeyManager.Current.AddOrReplace("Down", Key.Down, ModifierKeys.None, OnDown);
                HotkeyManager.Current.AddOrReplace("Tab", Key.Tab, ModifierKeys.None, OnSelect);
                HotkeyManager.Current.AddOrReplace("Return", Key.Return, ModifierKeys.None, OnSelect);
            }
            catch(Exception)
            {

            }
        }

        void unregistry()
        {
            try
            {
                HotkeyManager.Current.Remove("Up");
                HotkeyManager.Current.Remove("Down");
                HotkeyManager.Current.Remove("Tab");
                HotkeyManager.Current.Remove("Return");
            }
            catch(Exception)
            {

            }
        }


        private void input_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            unregistry();
            MainViewModel.Instance.AssCmdVM.Show = Visibility.Hidden;
        }

        private void input_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            registry();
        }

        #endregion

    }
}
