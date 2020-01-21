using MapComparer.Model;
using MapComparer.view;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MapComparer.viewmodel {

    class Window1ViewModel : BindableObject {

        private ObservableCollection<Texture> textures;

        public  ObservableCollection<Texture> Textures {
            get => textures;
            set { textures = value; OnPropertyChanged(); }
        }


        public Window1ViewModel() {
            textures = TextureManager.texturesOld;
        }


        public ICommand ScanTextures {
            get {
                return new Command(
                        (obj) => new ScanWindow().ShowDialog()
                    );
            }
        }

    }

}
