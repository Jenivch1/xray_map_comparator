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

namespace MapComparer.view {

    public partial class ScanWindow : Window {

        public ScanWindow() {
            InitializeComponent();

            Loaded += delegate { Application.Current.MainWindow.Effect = new System.Windows.Media.Effects.BlurEffect() { Radius = 4 }; };
            Closed += delegate { Application.Current.MainWindow.Effect = null; };
        }
        
    }
}
