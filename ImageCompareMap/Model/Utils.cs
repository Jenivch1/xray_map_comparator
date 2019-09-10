using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapComparer.Model
{
    class Utils
    {
        public static string PickFolder ()
        {
            return 
                (new System.Windows.Forms.FolderBrowserDialog())
                .SelectedPath;
        }
    }
}
