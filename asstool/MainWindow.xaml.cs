﻿using asstool.ViewModel;
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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

            InitializeComponent();

            set_theme_and_accent();

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
        }


    }
}
