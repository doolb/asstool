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

        private int orgIndex;
        public int OrgIndex { 
            get { return orgIndex; } 
            set { orgIndex = value;
            if (value <= key_start) 
                AssCmdVM.Show = Visibility.Hidden;
            } 
        }

        private string orgAssCode;
        public  string OrgAssCode
        {
            get { return orgAssCode; }
            set
            {
                orgAssCode = value;
                RaisePropertyChanged();

                try
                {
                    ass_assistx();

                    // convert to html code
                    AssCode = AssVisual.AssToHtml(value);
                }
                catch(Exception)
                {

                }
            }
        }

        private string assCode;
        public  string AssCode
        {
            get { return assCode; }
            set
            {
                assCode = value;
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

            AssCode = AssVisual.HtmlStyle;
        }

        #region function
        int key_start;
        void ass_assistx()
        {
            if(AssCmdVM.Show == Visibility.Visible)
            {
                string key = orgAssCode.Substring(key_start+1, orgAssCode.Length - key_start -1);
                AssCmdVM.Input = key.Split('{', '}', ',', '(', ')')[0];
            }

            if(orgAssCode[orgIndex] == '\\')
            {
                key_start = orgIndex;
                AssCmdVM.Show = Visibility.Visible;
                AssCmdVM.Input = "";
            }
        }

        #endregion
    }
}
