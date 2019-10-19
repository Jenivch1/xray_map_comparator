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
    class StartPageViewModel : BindableObject
    {
        private TextureScanner model;
        public TextureScanner Model
        {
            get
            {
                return model;
            }
            set
            {
                model = value; OnPropertyChanged();
            }
        }

        private ObservableCollection<Texture> textures;
        public ObservableCollection<Texture> Textures
        {
            get
            {
                return textures;
            }
            set
            {
                textures = value;
                OnPropertyChanged();
            }
        }

        public StartPageViewModel ()
        {
            Model = TextureScanner.Instance;
            //textures = TextureManager.texturesOld;
        }

        public ICommand ScanTextures
        {
            get
            {
                return new Command(
                        (obj) => new Interface.ScanDialog().ShowDialog()
                    );
            }
        }

    }
}