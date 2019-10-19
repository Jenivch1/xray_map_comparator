using mapcompare.Utilites;
using MapComparer.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace MapComparer.Viewmodel
{
    class MainWindowViewModel : BindableObject
    {
        // turn to auto prop?
        private TextureScanner model;
        public TextureScanner Model {
            get
            {
                return model;
            }
            private set
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

        public ObservableCollection<ITexture> OldTextures    { get; set; }
        public ObservableCollection<ITexture> NewTextures    { get; set; }

#if DEBUG
        public MainWindowViewModel ()
        {
            //
        }
#endif
        public MainWindowViewModel(TextureScanner scanner)
        {
            Model = scanner;
            OldTextures = Model.TexturesOld;
            NewTextures = Model.TexturesNew;
        }

        public ICommand ScanTextures
        {
            get
            {
                return new Command(
                        (obj) => {
                            var dialog = WindowFactory.CreateScanDialog();
                            dialog.ShowDialog();
                        }
                    );
            }
        }

        public ICommand SetSimilarAsMatch
        {
            get
            {
                return new Command(
                        (obj) => {
                            var match = obj as Texture;
                            if  (selectedTexture.Match == match)    { SelectedTexture.SetMatch(null); }
                            else                                    { SelectedTexture.SetMatch(obj as Texture); }
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
                        (obj) => OldTextures.Remove(obj as Texture),
                        (obj) => obj != null && obj is ITexture
                    );
            }
        }

        public ICommand ExportTexList
        {
            get
            {
                return new Command(
                        (obj) => SimilarTexturesListExporter.Export(OldTextures),
                        (obj) => OldTextures.Count > 0
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
                            listbox.SelectedIndex = 
                                (listbox.SelectedIndex == listbox.Items.Count - 1 || listbox.SelectedIndex == -1)
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
                            //var listbox = obj as ListBox;
                            //listbox.SelectedIndex = (listbox.SelectedIndex == listbox.Items.Count - 1 || listbox.SelectedIndex == -1)
                            //    ? 0
                            //    : listbox.SelectedIndex + 1;

                            IncrementSelectorSelectedItem(obj as Selector);
                        },
                        (obj) => obj is Selector
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

        private void IncrementSelectorSelectedItem (Selector control)
        {
            //if
            control.SelectedIndex = 
                (control.SelectedIndex == control.Items.Count - 1 || control.SelectedIndex == -1)
                                ? 0
                                : control.SelectedIndex + 1;
        }

        private void DecrementSelectorSelectedItem(Selector control)
        {
            if (!control.Items.IsEmpty)
            {
                control.SelectedIndex = (control.SelectedIndex == 0 || control.SelectedIndex == -1)
                    ? control.Items.Count - 1
                    : control.SelectedIndex - 1;
            }
        }
    }
}