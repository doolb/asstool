﻿using asstool.Model;
using MahApps.Metro;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Setting = asstool.Properties.Settings;

namespace asstool.ViewModel
{
    public class ThemeViewModel : BaseViewModel
    {
        public List<AccentColorMenuData> AccentColors { get; set; }
        public List<AppThemeMenuData> AppThemes { get; set; }

        public List<CultureInfo> CultureInfos { get; set; }

        public ThemeViewModel()
        {
            // get accent list
            AccentColors = ThemeManager.Accents.Select(
                a => new AccentColorMenuData()
                {
                    Name = a.Name,
                    ColorBrush = a.Resources["AccentColorBrush"] as Brush
                }).ToList();

            // get app theme list
            AppThemes = ThemeManager.AppThemes.Select(
                a => new AppThemeMenuData()
                {
                    Name = a.Name,
                    BorderColorBrush = a.Resources["BlackColorBrush"] as Brush,
                    ColorBrush = a.Resources["WhiteColorBrush"] as Brush
                }).ToList();

            CultureInfos = CultureInfo.GetCultures(CultureTypes.InstalledWin32Cultures).ToList();
            
        }
    }

    public class AccentColorMenuData
    {
        public string Name { get; set; }
        public Brush BorderColorBrush { get; set; }
        public Brush ColorBrush { get; set; }

        private ICommand changeAccentCommand;

        public ICommand ChangeAccentCommand
        {
            get
            {
                return this.changeAccentCommand ?? (changeAccentCommand =
                    new BaseCommand
                    {
                        ExecuteDelegate = x => this.DoChangeTheme(x)
                    });
            }
        }

        protected virtual void DoChangeTheme(object sender)
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            var accent = ThemeManager.GetAccent(this.Name);
            ThemeManager.ChangeAppStyle(Application.Current, accent, theme.Item1);

            // store current accent name, so it can be auto load when application start next time
            Setting.Default.color = this.Name;
            Setting.Default.Save();
        }
    }

    public class AppThemeMenuData : AccentColorMenuData
    {
        protected override void DoChangeTheme(object sender)
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            var appTheme = ThemeManager.GetAppTheme(this.Name);
            ThemeManager.ChangeAppStyle(Application.Current, theme.Item2, appTheme);


            // store current theme name, so it can be auto load when application start next time
            Setting.Default.theme = this.Name;
            Setting.Default.Save();
        }
    }
}
