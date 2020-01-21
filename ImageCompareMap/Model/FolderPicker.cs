using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mapcompare.Model
{
    static class FolderPicker
    {
        private static string Pick ()
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            return dialog.SelectedPath;
        }
    }
}
