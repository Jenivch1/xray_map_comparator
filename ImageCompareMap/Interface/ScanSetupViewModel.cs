using MapComparer.Model;
using System.Windows.Input;

namespace MapComparer.Viewmodel
{
    class ScanSetupViewModel : BindableObject
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

        private short resolution;
        public short Resolution
        {
            get => resolution;
            set { resolution = value; OnPropertyChanged(); }
        }

        private byte threshold;
        public byte Threshold
        {
            get => threshold;
            set { threshold = value; OnPropertyChanged(); }
        }

        public ScanSetupViewModel (TextureScanner scanner)
        {
            Model           = scanner;
            
            Resolution      = Texture.previewSize;
            Threshold       = Hash.Threshold;
        }

        public ICommand ScanTextures
        {
            get
            {
                return new Command(
                        (obj) => {
                            /// TODO: Remove all logic from VM.
                            Hash.Threshold = Threshold;
                            Texture.previewSize = Resolution;
                            //PageManager.SetCurrentPage
#if DEBUG
                            Model.OldTexturesPath = @"C:\Users\User\Downloads\textures\textures_soc\crete";
                            Model.NewTexturesPath = @"C:\Users\User\Downloads\textures\textures_cs\crete";
#endif

                            Model.ScanTextures();
                            // Close scan dialog here.
                        }
                    );
            }
        }

        public ICommand GetOldPath
        {
            get
            {
                return new Command(
                        (obj) => { Model.OldTexturesPath = DirectoryPicker.Pick(); }
                    );
            }
        }

        public ICommand GetNewPath
        {
            get
            {
                return new Command(
                        (obj) => { Model.NewTexturesPath = DirectoryPicker.Pick(); }
                    );
            }
        }

    }

}