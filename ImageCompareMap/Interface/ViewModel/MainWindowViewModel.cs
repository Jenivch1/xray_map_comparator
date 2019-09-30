using MapComparer.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace MapComparer.Viewmodel
{
    class MainWindowViewModel : Notifyable
    {
        public ObservableCollection<Texture> OldTextures    { get; set; }
        public ObservableCollection<Texture> NewTextures    { get; set; }

        public MainWindowViewModel ()
        {
            OldTextures = TextureManager.texturesOld;
            NewTextures = TextureManager.texturesNew;
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

            //public ICommand SetMatch
            //{
            //    get
            //    {
            //        return new Command(
            //                (obj) => SelectedTexture.SetMatch(obj as Texture),
            //                (obj) => obj != null
            //            );
            //    }
            //}

        public ICommand RemSelected
        {
            get
            {
                return new Command(
                        (obj) => TextureManager.RemoveTexture(obj as Texture),
                        (obj) => obj != null
                    );
            }
        }

        public ICommand ExportTexList
        {
            get
            {
                return new Command(
                        (obj) => TextureManager.ExportTextureList()
                        //,check
                    );
            }
        }

    }
}