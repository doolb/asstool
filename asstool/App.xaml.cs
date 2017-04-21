using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using Setting = asstool.Properties.Settings;

namespace asstool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        App()
        {
            if (Setting.Default.is_run_first)
                run_first();

            // get command line
            string[] cmd = Environment.GetCommandLineArgs();

            // set language
            // resources must be set public
            /* 
             * localization
             * refer:https://www.tutorialspoint.com/wpf/wpf_localization.htm
             */
            if (cmd.Count() > 1)
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture =
                new System.Globalization.CultureInfo(cmd[1]);
            }
            else
            System.Threading.Thread.CurrentThread.CurrentUICulture =
                new System.Globalization.CultureInfo(Setting.Default.lang);

            
        }

        void run_first()
        {
            // set application if the first run

            /*
             * set language
             */
            string l = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
            switch (l)
            {
                case "en-US": Setting.Default.lang = "en";    break;
                case "zh-CN": Setting.Default.lang = "zh-CN"; break;
                case "zh-TW": Setting.Default.lang = "zh-CN"; break;


                default: Setting.Default.lang = "en"; break;
            }



            /*
             * update setting
             * refer:https://social.msdn.microsoft.com/Forums/vstudio/en-US/0d86ddc6-83c3-49fd-a478-fbc9b032dc8b/how-to-set-application-setting-in-wpf?forum=wpf
             */
            Setting.Default.is_run_first = false;
            Setting.Default.Save();
        }
    }

}
