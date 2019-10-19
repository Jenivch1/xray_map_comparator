using MapComparer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mapcompare.Utilites
{
    class SimilarTexturesListExporter
    {
        public static void Export(ObservableCollection<ITexture> oldTextures)
        {
            var savePath        = string.Empty;
            var texturePaths    = new Dictionary<string, string>();
            var align           = 10;
            var builder         = new StringBuilder("[new_texture_paths]");

            //pick folder
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                savePath = dialog.SelectedPath + @"\new_texture_paths.ini";
            }
            else 
            {
                return;
            }

            //collect matches
            foreach (var texture in oldTextures)
            {
                if (texture.Match != null)
                {
                    var keyName = Path.GetFileNameWithoutExtension(texture.Path);
                    var keyDir  = Path.GetFileName(Path.GetDirectoryName(texture.Path));
                    var key     = Path.Combine(keyDir, keyName);

                    var valName = Path.GetFileNameWithoutExtension(texture.Match.Path);
                    var valDir  = Path.GetFileName(Path.GetDirectoryName(texture.Match.Path));
                    var value   = Path.Combine(valDir, valName);

                    texturePaths.Add(key, value);
                }
            }

            //check matches exist
            if (texturePaths.Count == 0)
            {
                MessageBox.Show("No texture matches to save!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //find align offset
            foreach (var key in texturePaths.Keys)
            {
                if (key.Length > align) align = key.Length;
            }

            //fill builder
            foreach (var key in texturePaths.Keys)
            {
                var value = texturePaths[key];
                builder.AppendFormat("\n{0, -" + align + "} = {1}", key, value);
            }

            //try save
            try
            {
                File.WriteAllText(savePath, builder.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
