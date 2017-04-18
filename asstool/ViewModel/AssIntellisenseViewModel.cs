using asstool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace asstool.ViewModel
{
    public class AssIntellisenseViewModel : BaseViewModel
    {
        public List<string> CmdList { get; set; }

        public Cmd Cmd { get; set; }

        private int index;
        public int Index
        {
            get { return index; }
            set
            {
                if (value >= 0 && value < CmdList.Count)
                {
                    index = value;
                    RaisePropertyChanged("Index");

                    Cmd = AssCmd.CmdDictionary[CmdList[index]];
                    RaisePropertyChanged("Cmd");
                }
            }
        }

        private Visibility show = Visibility.Hidden;
        public Visibility Show
        {
            get { return show; }
            set { show = value; RaisePropertyChanged(); }
        }
        
        #region ass intellisense position
        private double assCmdLeft = 100;
        public double AssCmdLeft 
        {
            get { return assCmdLeft; }
            set { assCmdLeft = value; RaisePropertyChanged(); }
        }

        private double assCmdTop = 100;
        public double AssCmdTop
        {
            get { return assCmdTop; }
            set { assCmdTop = value; RaisePropertyChanged(); }
        }
        #endregion


        public AssIntellisenseViewModel()
        {
            CmdList = AssCmd.CmdDictionary.Keys.ToList();
            RaisePropertyChanged("CmdList");

            Cmd = AssCmd.CmdDictionary[CmdList[0]];
            RaisePropertyChanged("Cmd");
        }
    }
}
