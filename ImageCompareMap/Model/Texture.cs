using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using MapComparer.Viewmodel;

namespace MapComparer.Model
{
    public class Texture : BindableObject
    {
        public static short previewSize     = 128;

        private BitmapImage preview;
        public BitmapImage Preview
        {
            get => preview;
            set { preview = value; OnPropertyChanged(); }
        }

        private string  path;
        public string Path
        {
            get => path;
            set { path = value; OnPropertyChanged(); }
        }

        private string  name;
        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(); }
        }

        private string  resolution;
        public string Resolution
        {
            get => resolution;
            set { resolution = value; OnPropertyChanged(); }
        }

        private BitArray hash;
        public BitArray Hash
        {
            get => hash;
            set { hash = value; OnPropertyChanged(); }
        }

        private Texture bestMatch;
        public Texture BestMatch
        {
            get => bestMatch;
            set { bestMatch = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Texture> Similar { get; set; }

        public Texture (Bitmap bitmap, string _path)
        {
            using (var thumbnail = new Bitmap(bitmap, new Size(previewSize, previewSize)))
            {
                preview = BitmapToImageSource(thumbnail);
                //var prev = new BitmapImage(new System.Uri(_path));
            }
            Hash        = Model.Hash.Create(bitmap);
            // Turn to Size
            resolution  = bitmap.HorizontalResolution.ToString() + "x" + bitmap.VerticalResolution.ToString();
            Path        = _path;
            Name        = System.IO.Path.GetFileNameWithoutExtension(_path);
            bestMatch   = null;
            Similar     = new ObservableCollection<Texture>();
        }

        private static BitmapImage BitmapToImageSource (Bitmap bitmap)
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

        public void SetMatch (Texture texture)
        {
            if (texture == null)    return;
            BestMatch = (BestMatch == texture) ? null : texture;
        }
    }
}
