using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace MapComparer.Model
{
    public interface ITexture
    {
        Texture BestMatch { get; set; }
        BitArray Hash { get; set; }
        string Name { get; set; }
        string Path { get; set; }
        BitmapImage Preview { get; set; }
        string Resolution { get; set; }
        ObservableCollection<Texture> Similar { get; set; }

        void SetMatch(Texture texture);
    }
}