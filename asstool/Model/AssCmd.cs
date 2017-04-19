using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;
using Setting = asstool.Properties.Settings;

namespace asstool.Model
{
    public class AssDocument
    {
        public static Dictionary<string, string> DocumentDictionary = new Dictionary<string, string>();

        static AssDocument()
        {
            string name = string.Format("ass.{0}.txt", Setting.Default.lang);
            string path = Path.Combine("ass", name);

            #region read ass document from file
            string key;
            StringBuilder sb = new StringBuilder();
            int line=0;
            using (StreamReader sr = new StreamReader(path))
            {
                while(!sr.EndOfStream)
                {
                    // read key
                    key = sr.ReadLine().TrimStart('\t', ' ').TrimEnd();
                    line++;
                    if (key.Length == 0) continue; // is empty line
                    if (key[0] == ';') continue;  // is comment 

                    // get the '{'
                    if (sr.ReadLine().Trim()[0] != '{')
                    {
                        MessageBox.Show(string.Format("ass document file error, key:[{0}] at line: {1}", key, line++));
                        continue;
                    }

                    // read doc
                    while(true)
                    {
                        if (sr.EndOfStream) break;
                        string s = sr.ReadLine().TrimStart().TrimEnd();
                        line++;
                        if (s.Length == 0) continue; // is empty line
                        if (s[0] == ';') continue;  // is comment 

                        if (s[0] != '}')
                        {
                            sb.Append(s);
                            continue;
                        }
                        else
                        {
                            DocumentDictionary.Add(key, sb.ToString());

                            sb.Clear();
                            break;
                        }
                    }
                    
                }
            }
            #endregion

        }
    }
}
