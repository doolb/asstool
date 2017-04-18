using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asstool.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private static MainViewModel instance;
        public static MainViewModel Instance
        {
            get { return instance ?? (instance = new MainViewModel()); }
        }

        public ThemeViewModel ThemeVM { get; set; }

        public AssIntellisenseViewModel AssCmdVM { get; set; }

        public MainViewModel()
        {
            ThemeVM = new ThemeViewModel();

            AssCmdVM = new AssIntellisenseViewModel();
        }
    }
}
