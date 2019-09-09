using MapComparer.Model;
using MapComparer.view;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace MapComparer.viewmodel {

    class MainWindowViewModel : AbstractVM {

        public ObservableCollection<Texture> Textures { get; set; }

        private Texture selectedTexture;
        public  Texture SelectedTexture {
            get => selectedTexture;
            set { selectedTexture = value; OnPropertyChanged(); }
        }

        private Texture selectedSimilar;
        public  Texture SelectedSimilar {
            get => selectedSimilar;
            set { selectedSimilar = value; OnPropertyChanged(); }
        }


        public MainWindowViewModel() {
            Textures = TextureManager.texturesOld;
        }


        public ICommand ScanTextures {
            get {
                return new Command(
                        (obj) => new ScanWindow().ShowDialog()
                    );
            }
        }

        public ICommand SetMatch {
            get {
                return new Command(
                        (obj) => SelectedTexture.SetMatch(SelectedSimilar),
                        (obj) => SelectedSimilar != null
                    );
            }
        }

        public ICommand RemSelected {
            get {
                return new Command(
                        (obj) => TextureManager.RemoveTexture(SelectedTexture)
                        //,
                        //(obj) => SelectedTexture != null
                    );
            }
        }

        public ICommand ExportTexList {
            get {
                return new Command(
                        (obj) => TextureManager.ExportTextureList()
                        //,
                        //check
                    );
            }
        }
        
    }

}