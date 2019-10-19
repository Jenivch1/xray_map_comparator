using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace MapComparer.Model
{
    public interface ITexture
    {
        ITexture                        Match           { get; set; }
        BitArray                        Hash            { get; }
        string                          Name            { get; }
        string                          Path            { get; }
        BitmapImage                     Preview         { get; }
        string                          Resolution      { get; }
        ObservableCollection<ITexture>  Similar         { get; set; }
        
        void SetMatch(ITexture texture);
    }
}