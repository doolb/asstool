using asstool.Model;
using asstool.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace asstool.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region instance
        private static MainViewModel instance;
        public static MainViewModel Instance
        {
            get { return instance ?? (instance = new MainViewModel()); }
        }
        #endregion

        #region child view model
        public ThemeViewModel ThemeVM { get; set; }

        public AssIntellisenseViewModel AssCmdVM { get; set; }
        #endregion

        #region simple property

        private string orgAssCode = "test";
        public  string OrgAssCode
        {
            get { return orgAssCode; }
            set
            {
                orgAssCode = value;
                RaisePropertyChanged();
            }
        }
        

        #endregion

        #region
        private ICommand showAbout;
        public  ICommand ShowAbout
        {
            get
            {
                return showAbout ?? (showAbout = new BaseCommand
                    {
                        ExecuteDelegate = x=>
                            {
                                new About().ShowDialog();
                            }
                    });
            }
        }
        #endregion
        public MainViewModel()
        {
            ThemeVM = new ThemeViewModel();

            AssCmdVM = new AssIntellisenseViewModel();

        }
    }
}
