using asstool.Model;
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
using Res = asstool.Properties.Resources;
using System.Diagnostics;
using System.Reflection;

namespace asstool.ViewModel
{
    public class ThemeViewModel : BaseViewModel
    {
        public List<AccentColorMenuData> AccentColors { get; set; }
        public List<AppThemeMenuData> AppThemes { get; set; }

        public List<CultureInfo> CultureInfos { get; set; }

        public List<LanguageMenuData> Languages { get; set; }

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

            Languages = LanguageMenuData.Languages;
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

    public class LanguageMenuData
    {
        public string Lang{get;set;}

        public string Name{get;set;}

        private ICommand changeLanguage;

        public ICommand ChangeLanguage
        {
            get 
            {
                return changeLanguage ??(changeLanguage = new BaseCommand
                {
                    ExecuteDelegate = async x=>
                        {
                            // just store language
                            // and ask for relaunch
                            MessageDialogResult ret =  await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync(
                                string.Format(Res.msg_change_language_title,this.Name),
                                Res.msg_change_language,
                                MessageDialogStyle.AffirmativeAndNegative);

                            if (ret == MessageDialogResult.Affirmative)
                            {
                                Setting.Default.lang = this.Lang;
                                Setting.Default.Save();

                                Process.Start(new ProcessStartInfo(Assembly.GetExecutingAssembly().Location, this.Lang));
                                Application.Current.Shutdown();

                            }
                        }
                });

            }
        }

        private static List<LanguageMenuData> languages;
        public  static List<LanguageMenuData> Languages
        {
            get
            {
                return languages;
            }
        }

        static LanguageMenuData()
        {
            languages = new List<LanguageMenuData>();

            languages.Add(new LanguageMenuData
            {
                Lang = "en",
                Name = "English"
            });
            languages.Add(new LanguageMenuData
            {
                Lang = "zh-CN",
                Name = "中文简体"
            });
            languages.Add(new LanguageMenuData
            {
                Lang = "zh-TW",
                Name = "中文繁體"
            });
        }
    }
}
