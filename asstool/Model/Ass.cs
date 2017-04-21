using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

            AllKey = DocumentDictionary.Keys.ToList();
            AllKey.Sort();
        }

        private static List<string> AllKey;

        public static List<string> Match(string pattern)
        {
            if (pattern == null) return null;

            if (pattern == "") return AllKey;

            // get match key            
            IEnumerable<string> match_keys = DocumentDictionary.Keys.Where(
                key => key.Contains(pattern));

            List<string> list = new List<string>();
            foreach(string key in match_keys)
            {
                list.Add(key);
            }

            list.Sort();
            return list;
        }
    }

    public class AssVisual
    {
        static int tab_index;
        
        public static void Reset()
        {
            tab_index = 0;
            sb.Clear();
        }

        static StringBuilder sb = new StringBuilder();

        public static string HtmlStyle = "<style>body {background-color: #252525;color: #bfbfbf;font-family: \"Times New Roman\", Georgia, Serif;}</style>";


        // "&#09;" equal '\t'
        public static string AssToHtml(string ass_code)
        {
            if(ass_code == null || ass_code == "") return HtmlStyle;

            Reset();

            // append style
            sb.Append(HtmlStyle);
            sb.Append("<b>");

            int i = 0;
            for (; i < ass_code.Length; i++)
            {
                char c = ass_code[i];
                switch (c)
                {
                    case '{':
                        sb.Append("<br><font color=\"#668cff\">{</font>");
                        tab_index++; insert_tab();
                        break;
                    case '}':
                        sb.Append("<br>"); // break line
                        sb.Append("<font color=\"#668cff\">}</font><br>");
                        tab_index--;
                        break;

                    case '\\':
                        sb.Append("<br>"); insert_tab();
                        sb.Append("<font color=\"#ff33ff\">\\</font>");
                        read_key(ref sb, ass_code, ref i);
                        read_param(ref sb, ass_code, ref i);
                        break;

                    case '(':
                        sb.Append("<font color=\"#ff33ff\">(</font><br>");
                        tab_index++; insert_tab();
                        read_param(ref sb, ass_code, ref i);
                        break;
                    case ')':
                        tab_index--; insert_tab();
                        sb.Append("<font color=\"#ff33ff\">)</font><br>");
                        break;
                    case ',':
                        sb.Append(" , ");
                        read_param(ref sb, ass_code, ref i);
                        break;

                    case '&':
                        sb.Append(" & ");
                        read_param_hex(ref sb, ass_code, ref i);
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }

            sb.Append("</b>");
            return sb.ToString();
        }


        internal static void insert_tab()
        {
            for (int i = 0; i < tab_index; i++)
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
        }

        internal static void read_key(ref StringBuilder html, string ass_code,ref int i)
        {
            html.Append("<font color=\"#D1D80A\">");

            char c;
            while(true)
            {
                c = ass_code[++i];
                if (!char.IsLetter(c)) break;
                html.Append(c);
            }
            html.Append("</font>");

            i--;
        }
        internal static void read_param(ref StringBuilder html, string ass_code,ref int i)
        {
            html.Append("<font color=\"#1CE32E\"> ");

            bool for_color = false;
            char c = ass_code[i];
            if (c == '&') for_color = true;
 
            while (true)
            {
                c = ass_code[++i];
                if (!char.IsDigit(c) && c != '.') break;
                html.Append(c);
            }

            if (for_color)
                html.Append(ass_code[++i]);
            html.Append("</font>");

            i--;
        }

        internal static void read_param_hex(ref StringBuilder html, string ass_code, ref int i)
        {
            html.Append("<font color=\"#0A8D1F\"> ");

            char c = ass_code[++i];
            if(char.IsDigit(c))
            {
                html.Append(c);
                html.Append(ass_code[i + 1]);

                i+=1;
            }
            else
            {
                html.Append(c);
                html.Append(" ");
                html.Append(ass_code[i + 1]);
                html.Append(ass_code[i + 2]);
                html.Append(" ");
                html.Append(ass_code[i + 3]);
                html.Append(ass_code[i + 4]);
                html.Append(" ");
                html.Append(ass_code[i + 5]);
                html.Append(ass_code[i + 6]);
                html.Append(" & ");

                i += 7;
            }
            html.Append("</font>");
        }

    }
}
