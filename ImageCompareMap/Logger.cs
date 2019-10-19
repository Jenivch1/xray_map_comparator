using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapComparer.Model
{
    
    static class Logger
    {
        private const string Error = "[Error] ";
        private static List<string> Messages;

        public static void Log(string message)
        {
            var time = "[" + DateTime.Now.ToString() + "] ";
            Messages.Add(time + message);
        }

        public static void LogError(string message)
        {
            Messages.Add("\n" + Error + message + "\n");
        }

        public static void ExportLogToFile()
        {

        }
    }
}
