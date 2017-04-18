using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asstool.Model
{
    public class Cmd
    {
        public string Name { get; set; }

        public string Format { get; set; }

        public string Example { get; set; }

        public string Comment { get; set; }
    }
    public class AssCmd
    {
        public static Dictionary<string, Cmd> CmdDictionary = new Dictionary<string, Cmd>();

        static AssCmd()
        {
            CmdDictionary.Add("fs", new Cmd()
            {
                Format = "\\fs<size>",
                Example = "\\fs50",
                Name = "Font Size"
            });

            CmdDictionary.Add("pos", new Cmd()
            {
                Format = "\\pos (<x>, <y>)",
                Example = "\\pos(470, 260)",
                Name = "Set position"
            });

            CmdDictionary.Add("move", new Cmd()
            {
                Format = "\\move (<x1>, <y1>, <x2>, <y2> [, <t1>, <t2>])",
                Example = "\\move (100, 150, 300, 350)",
                Name = "Movement"
            });
        }
    }
}
