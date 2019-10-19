using MapComparer.Interface;
using MapComparer.Model;
using MapComparer.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MapComparer
{
    /// maybe ViewFactory?
    static class WindowFactory
    {
        public static Window CreateMainWindow()
        {
            var model       = TextureScanner.Instance;
            var viewmodel   = new MainWindowViewModel(model);
            var window      = new MainWindow() { DataContext = viewmodel };
            return window;
        }

        public static Window CreateScanDialog ()
        {
            var model       = TextureScanner.Instance;
            var viewmodel   = new ScanSetupViewModel(model);
            var scanDialog  = new ScanDialog() { DataContext = viewmodel};
            return scanDialog;
        }
    }
}