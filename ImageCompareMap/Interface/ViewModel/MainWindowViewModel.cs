using MapComparer.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MapComparer.Viewmodel
{

    /// commands
    /// 
    /// next imame
    /// prev image
    /// next similar
    /// prev similar
    /// set match
    /// add to matches


    class MainWindowViewModel : BindableObject
    {
        private TextureManager model;
        public TextureManager Model {
            get
            {
                return model;
            }
            set
            {
                model = value;
                OnPropertyChanged();
            }
        }

        private Texture selectedTexture;
        public Texture SelectedTexture
        {
            get
            {
                return selectedTexture;
            }
            set
            {
                selectedTexture = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Texture> OldTextures    { get; set; }
        public ObservableCollection<Texture> NewTextures    { get; set; }

        public MainWindowViewModel()
        {
            Model = TextureManager.Instance;
            //OldTextures = TextureManager.texturesOld;
            //NewTextures = TextureManager.texturesNew;
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

        public ICommand SetSimilarAsMatch
        {
            get
            {
                return new Command(
                        (obj) => {
                            SelectedTexture.SetMatch(obj as Texture);
                        },
                        (obj) => (obj != null && obj is Texture)
                    );
            }
        }

        public ICommand RemoveSelected
        {
            get
            {
                return new Command(
                        (obj) => Model.RemoveTexture(obj as Texture),
                        (obj) => obj != null
                    );
            }
        }

        public ICommand ExportTexList
        {
            get
            {
                return new Command(
                        (obj) => Model.ExportTextureList()
                        //,check
                    );
            }
        }


        public ICommand SelectNextImage
        {
            get
            {
                return new Command(
                        (obj) =>
                        {
                            var listbox = obj as ListBox;
                            listbox.SelectedIndex = (listbox.SelectedIndex == listbox.Items.Count - 1 || listbox.SelectedIndex == -1)
                                ? 0
                                : listbox.SelectedIndex + 1;
                        }
                        //,
                        //(obj) => 
                        //{
                        //    return (obj is ListBox) && !(obj as ListBox).Items.IsEmpty;
                        //}
                    );
            }
        }

        /// Add: Seelect irst similar if we have one.
        public ICommand SelectPrevImage
        {
            get
            {
                return new Command(
                        (obj) =>
                        {
                            if (obj is ListBox)
                            {
                                var listbox = obj as ListBox;
                                if (!listbox.Items.IsEmpty)
                                {
                                    listbox.SelectedIndex = (listbox.SelectedIndex == 0 || listbox.SelectedIndex == -1)
                                        ? listbox.Items.Count - 1
                                        : listbox.SelectedIndex - 1;
                                }
                            }
                        }
                    );
            }
        }

        public ICommand SelectNextSimilarImage
        {
            get
            {
                return new Command(
                        (obj) =>
                        {
                            var listbox = obj as ListBox;
                            listbox.SelectedIndex = (listbox.SelectedIndex == listbox.Items.Count - 1 || listbox.SelectedIndex == -1)
                                ? 0
                                : listbox.SelectedIndex + 1;
                        }
                    );
            }
        }

        public ICommand SelectPrevSimilarImage
        {
            get
            {
                return new Command(
                        (obj) =>
                        {
                            if (obj is ListBox)
                            {
                                var listbox = obj as ListBox;
                                if (!listbox.Items.IsEmpty)
                                {
                                    listbox.SelectedIndex = (listbox.SelectedIndex == 0 || listbox.SelectedIndex == -1)
                                        ? listbox.Items.Count - 1
                                        : listbox.SelectedIndex - 1;
                                }
                            }
                        }
                    );
            }
        }
    }
}