using MapComparer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MapComparer.Viewmodel
{
    class Window1ViewModel : AbstractVM
    {
        private ObservableCollection<Texture> textures;
        public ObservableCollection<Texture> Textures
        {
            get => textures;
            set { textures = value; OnPropertyChanged(); }
        }

        public Window1ViewModel ()
        {
            textures = TextureManager.texturesOld;
        }

        //public ICommand ScanTextures {
        //    get {
        //        return new Command(
        //                (obj) => new ScanDialog().ShowDialog()
        //            );
        //    }
        //}

    }
}