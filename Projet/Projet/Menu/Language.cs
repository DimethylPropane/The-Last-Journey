using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet
{
    class Language
    {
        private static string[] items;

        public static void Initialize()
        {
            string line;
            string lang;
            System.IO.StreamReader file = new System.IO.StreamReader("config.ini");
            line = file.ReadLine();
            lang = line.Substring(9);
            file.Dispose();

            if (lang == "french")
            {
                items = System.IO.File.ReadAllLines("frFR.txt");
            }

            else
            {
                items = System.IO.File.ReadAllLines("enGB.txt");
            }
        }

        public static string GetWord(string id)
        {
            string error = "UNASSIGNED";
            int strIndex = 0;
            int iter = 0;
            while (iter < items.Length)
            {
                strIndex = items[iter].IndexOf(id);
                if (strIndex >= 0)
                    break;

                if (iter == items.Length - 1)
                    return error;

                iter++;
            }

            string result = items[iter].Substring(id.Length + 3);
            return result.Trim( new Char[] {'"'} );
        }
    }
}
