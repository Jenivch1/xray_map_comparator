using MapComparer.Model;
using System.Windows.Input;

namespace MapComparer.Viewmodel
{
    class ScanSetupViewModel : BindableObject
    {
        private TextureManager model;
        public TextureManager Model
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

        public ScanSetupViewModel ()
        {
            Model           = TextureManager.Instance;

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

                            Hash.Threshold         = Threshold;
                            Texture.previewSize    = Resolution;
                            PageManager.SetFramePage(Pages.Main);

                            Model.ProcessTextures();
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
                        (obj) => { Model.OldTexturesPath = Utils.PickFolder(); }
                    );
            }
        }

        public ICommand GetNewPath
        {
            get
            {
                return new Command(
                        (obj) => { Model.NewTexturesPath = Utils.PickFolder(); }
                    );
            }
        }

    }

}