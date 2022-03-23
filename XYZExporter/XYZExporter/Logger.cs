using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZExporter
{
    public class Logger
    {
        private static string _ResultPath;
        public static string ResultPath { get { return _ResultPath; } set { _ResultPath = value + "/logs.txt"; } }
        public static void Log(List<Student> students)
        {
            using (StreamWriter sw = new StreamWriter(_ResultPath))
            {
                foreach (Student student in students)
                {
                    string line = ":Wrong data - For student with index s" + student.Index;
                    sw.WriteLine(line);
                }
            }
        }

        public static void Log(string msg)
        {
            using (StreamWriter sw = new StreamWriter(_ResultPath))
            {
                sw.WriteLine(msg);
            }
        }
    }
}
