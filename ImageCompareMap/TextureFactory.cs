using MapComparer.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MapComparer.Model
{
    static class TextureFactory
    {
        public static ITexture CreateTexture(Bitmap bitmap, string texturePath)
        {
            var hash    = Hash.Create(bitmap);
            var texture = new Texture(bitmap, "", hash);
            return texture;
        }

        //public static ITexture CreateTexture(BitmapImage bitmap, string texturePath)
        //{
        //    var hash = Hash.Create(bitmap);
        //    var texture = new Texture(bitmap, "", hash);
        //    return texture;
        //}
    }
}