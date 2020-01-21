using MapComparer.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace MapComparer.viewmodel
{

    class ScanSetupViewModel : BindableObject
    {
        private string  oldTexPath;
        public string OldTexPath
        {
            get => oldTexPath;
            set { oldTexPath = value; OnPropertyChanged(); }
        }

        private string  newTexPath;
        public string NewTexPath
        {
            get => newTexPath;
            set { newTexPath = value; OnPropertyChanged(); }
        }

        private short   resolution;
        public short Resolution
        {
            get => resolution;
            set { resolution = value; OnPropertyChanged(); }
        }

        private byte    threshold;
        public byte Threshold
        {
            get => threshold;
            set { threshold = value; OnPropertyChanged(); }
        }

        private string  ignoredSubPaths;
        public string IgnoredSubPaths
        {
            get => ignoredSubPaths;
            set { ignoredSubPaths = value; OnPropertyChanged(); }
        }

        private string  ignoredTextures;
        public string IgnoredTextures
        {
            get => ignoredTextures;
            set { ignoredTextures = value; OnPropertyChanged(); }
        }

        public ScanSetupViewModel ()
        {
            oldTexPath      = Storage.OldTexturesPath;
            newTexPath      = Storage.NewTexturesPath;
            resolution      = Storage.PreviewResolution;
            threshold       = Storage.MatchThreshold;
            ignoredSubPaths = Storage.IgnoredSubfolders;
            ignoredTextures = Storage.IgnoredTextures;
        }

        public ICommand ScanTextures
        {
            get
            {
                return new Command(
                        (obj) => TextureManager.ProcessTextures()
                    );
            }
        }

        public ICommand GetOldPath
        {
            get
            {
                return new Command(
                        (obj) => Storage.GetOldPath()
                    );
            }
        }

        public ICommand GetNewPath
        {
            get
            {
                return new Command(
                        (obj) => Storage.GetNewPath()
                    );
            }
        }

    }

}
