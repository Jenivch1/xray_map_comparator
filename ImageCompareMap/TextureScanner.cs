using Imaging.DDSReader;
using mapcompare.Utilites;
using MapComparer.Viewmodel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MapComparer.Model
{
    class TextureScanner : BindableObject
    {
        private static TextureScanner instance;
        public static TextureScanner Instance
        {
            get
            {
                if (instance == null) { instance = new TextureScanner(); }
                return instance;
            }
            private set => instance = value;
        }

        private string newTexturesPath;
        public string NewTexturesPath
        {
            get => newTexturesPath;
            set { newTexturesPath = value; OnPropertyChanged(); }
        }

        private string oldTexturesPath;
        public string OldTexturesPath
        {
            get => oldTexturesPath;
            set { oldTexturesPath = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ITexture> texturesOld;
        public ObservableCollection<ITexture> TexturesOld
        {
            get => texturesOld;
            set { texturesOld = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ITexture> texturesNew;
        public ObservableCollection<ITexture> TexturesNew
        {
            get => texturesNew;
            set { texturesNew = value; OnPropertyChanged(); }
        }


        private TextureScanner()
        {
            TexturesOld = new ObservableCollection<ITexture>();
            TexturesNew = new ObservableCollection<ITexture>();
        }


        private ObservableCollection<ITexture> LoadTextures(string directoryPath)
        {
            string[] imagePaths = Directory.GetFiles(directoryPath, "*.dds", SearchOption.AllDirectories);
            var textures = new ObservableCollection<ITexture>();

            foreach (var imagePath in imagePaths)
            {
                if (TexturePathValidator.Validate(directoryPath, imagePath))
                {
                    var bitmap = ImageLoader.Load(imagePath);
                    
                    if (bitmap != null)
                    {
                        ITexture texture = TextureFactory.CreateTexture(bitmap, imagePath);
                        textures.Add(texture);
                    }
                    else { continue; }
                }
            }

            return textures;
        }

        public void RemoveTexture(Texture texture)
        {
            TexturesOld.Remove(texture);
        }

        private void FindSimilarTextures()
        {
            foreach (var texture in TexturesOld)
            {
                foreach (var newTexture in TexturesNew)
                {
                    if (Hash.IsSimilar(texture.Hash, newTexture.Hash))
                    {
                        texture.Similar.Add(newTexture);
                    }
                }
            }
        }

        public void ScanTextures()
        {
            TexturesOld = LoadTextures(OldTexturesPath);
            TexturesNew = LoadTextures(NewTexturesPath);
            FindSimilarTextures();
            MessageBox.Show(TexturesOld.Count.ToString());
            MessageBox.Show(TexturesNew.Count.ToString());
        }
    
    }

}