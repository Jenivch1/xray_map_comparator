using MapComparer.Model;
using MapComparer.Viewmodel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapComparer
{
    class Storage : BindableObject
    {
        public ObservableCollection<ITexture> Textures                  { get; set; }
        public ObservableCollection<ITexture> NewTextures               { get; set; }
        public ObservableCollection<ITexture> IgnoredSubdirectories     { get; set; }
        public ObservableCollection<ITexture> IgnoredTextures           { get; set; }

        static Storage instance;

        private Storage()
        {
            //
        }

        static internal Storage Instance
        {
            get 
            {
                if (instance == null) { instance = new Storage(); }
                return instance;
            } 
        }
    }
}