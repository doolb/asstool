using asstool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Xceed.Wpf.Toolkit;

namespace asstool.ViewModel
{
    public class AssIntellisenseViewModel : BaseViewModel
    {
        public List<string> Cmds { get; set; }

        private string document;
        public string Document { get { return document; } set { document = value; RaisePropertyChanged(); } }

        private int index;
        public int Index
        {
            get { return index; }
            set
            {
                if (value >= 0 && value < Cmds.Count)
                {
                    index = value;
                    RaisePropertyChanged("Index");

                    document = AssDocument.DocumentDictionary[Cmds[index]];
                    RaisePropertyChanged("Document");
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

        private double assCmdTop = 0;
        public double AssCmdTop
        {
            get { return assCmdTop; }
            set { assCmdTop = value; RaisePropertyChanged(); }
        }
        #endregion


        public AssIntellisenseViewModel()
        {
            try
            {
                Cmds = AssDocument.DocumentDictionary.Keys.ToList();
                Cmds.Sort();
            } 
            catch(Exception)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Ass document file lost.");
                Environment.Exit(-1);
            }
            RaisePropertyChanged("Cmds");

            this.Index = 0;
        }
    }
}
