using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using MapComparer.Viewmodel;

namespace MapComparer.Model
{
    public class Texture : BindableObject, ITexture
    {
        public static short previewSize = 128;

        private BitmapImage preview;
        public BitmapImage Preview
        {
            get => preview;
            private set { preview = value; OnPropertyChanged(); }
        }

        private string path;
        public string Path
        {
            get => path;
            private set { path = value; OnPropertyChanged(); }
        }

        private string name;
        public string Name
        {
            get => name;
            private set { name = value; OnPropertyChanged(); }
        }

        private string resolution;
        public string Resolution
        {
            get => resolution;
            private set { resolution = value; OnPropertyChanged(); }
        }

        private BitArray hash;
        public BitArray Hash
        {
            get => hash;
            private set { hash = value; OnPropertyChanged(); }
        }

        private ITexture match;
        public ITexture Match
        {
            get => match;
            set { match = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ITexture> Similar { get; set; }

        public Texture(Bitmap bitmap, string path, BitArray hash)
        {
            using (var thumbnail = new Bitmap(bitmap, new Size(previewSize, previewSize)))
            {
                preview = BitmapToImageSource(thumbnail);
                //var prev = new BitmapImage(new System.Uri(_path));
            }

            Hash        = hash;
            // Turn resol to Size
            //var size = new Size(242, 424);
            //Resolution  = bitmap.HorizontalResolution.ToString() + "x" + bitmap.VerticalResolution.ToString();
            Path        = path;
            Name        = System.IO.Path.GetFileNameWithoutExtension(path);
            Match       = null;
            Similar     = new ObservableCollection<ITexture>();
        }

        private static BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            BitmapImage bitmapImage = null;
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }
            return bitmapImage;
        }

        public void SetMatch(ITexture texture)
        {
            if (texture == null) return;
            Match = (Match == texture) ? null : texture;
        }
    }
}
