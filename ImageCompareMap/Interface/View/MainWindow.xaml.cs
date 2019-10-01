using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MapComparer.Model;

namespace MapComparer.Interface
{
    public partial class MainWindow : Window
    {
        public MainWindow ()
        {
            InitializeComponent();

            PageManager.frame = frmWindowContent;
            //PageManager.SetFramePage(Pages.Start);

            PageManager.SetFramePage(Pages.Main);

        }
    }
}