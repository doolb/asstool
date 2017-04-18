using asstool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public AssIntellisenseViewModel()
        {
            CmdList = AssCmd.CmdDictionary.Keys.ToList();
            RaisePropertyChanged("CmdList");

            Cmd = AssCmd.CmdDictionary[CmdList[0]];
            RaisePropertyChanged("Cmd");
        }
    }
}
