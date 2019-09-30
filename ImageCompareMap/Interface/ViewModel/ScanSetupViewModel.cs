using MapComparer.Model;
using System.Windows.Input;

namespace MapComparer.Viewmodel
{
    class ScanSetupViewModel : Notifyable
    {
        private string oldTexPath;
        public string OldTexPath
        {
            get => oldTexPath;
            set { oldTexPath = value; OnPropertyChanged(); }
        }

        private string newTexPath;
        public string NewTexPath
        {
            get => newTexPath;
            set { newTexPath = value; OnPropertyChanged(); }
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

        private string ignoredSubPaths;
        public string IgnoredSubPaths
        {
            get => ignoredSubPaths;
            set { ignoredSubPaths = value; OnPropertyChanged(); }
        }

        private string ignoredTextures;
        public string IgnoredTextures
        {
            get => ignoredTextures;
            set { ignoredTextures = value; OnPropertyChanged(); }
        }

        public ScanSetupViewModel ()
        {
            //oldTexPath      = Storage.OldTexturesPath;
            //newTexPath      = Storage.NewTexturesPath;
            //resolution      = Storage.PreviewResolution;
            //threshold       = Storage.MatchThreshold;
            //ignoredSubPaths = Storage.IgnoredSubfolders;
            //ignoredTextures = Storage.IgnoredTextures;

            OldTexPath      = TextureManager.OldTexturesPath;
            NewTexPath      = TextureManager.NewTexturesPath;
            Resolution      = Texture.previewSize;
            Threshold       = Hash.Threshold;
            IgnoredTextures = TextureManager.IgnoredTextures;
            IgnoredSubPaths = TextureManager.IgnoredTextures;
        }

        public ICommand ScanTextures
        {
            get
            {
                return new Command(
                        (obj) => {
                            /// TODO: Remove all logic from VM.
                            TextureManager.OldTexturesPath  = OldTexPath;
                            TextureManager.NewTexturesPath  = NewTexPath;
                            Hash.Threshold                  = Threshold;
                            Texture.previewSize             = Resolution;
                            TextureManager.ProcessTextures();
                            PageManager.SetFramePage(Pages.Main);
                            // Close scan dialog.
                        }
                    );
            }
        }

        public ICommand GetOldPath
        {
            get
            {
                return new Command(
                        (obj) => OldTexPath = Utils.PickFolder()
                    );
            }
        }

        public ICommand GetNewPath
        {
            get
            {
                return new Command(
                        (obj) => NewTexPath = Utils.PickFolder()
                    );
            }
        }

    }

}