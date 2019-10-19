using Imaging.DDSReader;
using MapComparer.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace mapcompare.Utilites
{
    class ImageLoader
    {
        public static Bitmap Load(string imagePath)
        {
            Bitmap bitmap = null;
            try
            {
                bitmap = DDS.LoadImage(imagePath);
            }
            catch (Exception e)
            {
                Logger.LogError($"Failed to load texture: {imagePath}. \n{e.Message}");
            }
            return bitmap;
        }

        public static BitmapImage LoadImage(string imagePath)
        {
            BitmapImage image = null;
            try
            {
                var uri = new Uri(imagePath);
                image   = new BitmapImage(uri);
            }
            catch (Exception e)
            {
                Logger.LogError($"Failed to load texture: {imagePath}. \n{e.Message}");
            }
            return image;
        }
    }
}