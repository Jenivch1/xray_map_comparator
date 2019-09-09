using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mapcompare.Model
{
    class Utils
    {
        private static string PickFolder ()
        {
            return 
                (new System.Windows.Forms.FolderBrowserDialog())
                .SelectedPath;
        }
    }
}
