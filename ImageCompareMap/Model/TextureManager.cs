using Imaging.DDSReader;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MapComparer.Model
{
    static class TextureManager
    {
        public static string                            OldTexturesPath     = @"C:\Users\User\Downloads\textures\textures_soc\crete";
        public static string                            NewTexturesPath     = @"C:\Users\User\Downloads\textures\textures_cs\crete";
        public static string                            IgnoredSubfolders   = @"act, andy, artifact, detail, glow, map, fx, pda, ed, hud, intro, icon, ui, internal, lights, pfx, terrain, sky, wm, water, wpn";
        public static string                            IgnoredTextures     = @"sunmask.dds, ui_icons_npc_old.dds, water_sbumpvolume.dds, bump, bump#";
        private static List<string>                     ignoredFolders;
        public static ObservableCollection<Texture>     texturesOld;
        private static ObservableCollection<Texture>    texturesNew;


        static TextureManager ()
        {
            ignoredFolders  = new List<string>();
            texturesOld     = new ObservableCollection<Texture>();
            texturesNew     = new ObservableCollection<Texture>();
        }


        static private void ParseIgnored ()
        {
            var temp = IgnoredSubfolders
                .ToLower()
                .Replace(" ", "")
                .Replace("\t", "")
                .Split(',');
            ignoredFolders = new List<string>(temp);
        }

        private static bool IsIgnored (string path)
        {
            var pathSubDir = Path.GetFileName(Path.GetDirectoryName(path.ToLower()));

            if (ignoredFolders.Contains(pathSubDir))
            {
                return true;
            }

            return false;
        }

        static private ObservableCollection<Texture> LoadTextures (string dir)
        {
            string[] paths = Directory.GetFiles(dir, "*.dds", SearchOption.AllDirectories);
            var textures = new ObservableCollection<Texture>();

            foreach (var file in paths)
            {
                if (IsIgnored(file)) continue;
                Bitmap bitmap = null;
                
                try
                {
                    bitmap = DDS.LoadImage(file);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\n[ERROR] Failed to load texture.\n{file}\n{e.Message}\n\n");
                    continue;
                }

                textures.Add(new Texture(bitmap, file));
                bitmap.Dispose();
            }

            return textures;
        }

        static public void RemoveTexture (Texture texture)
        {
            texturesOld.Remove(texture);
        }

        static private void FindSimilarTextures ()
        {
            foreach (var texture in texturesOld)
            {
                foreach (var newTexture in texturesNew)
                {
                    if (Hash.IsSimilar(texture.Hash, newTexture.Hash))
                    {
                        texture.Similar.Add(newTexture);
                    }
                }
            }
        }

        public static void ProcessTextures ()
        {
            texturesOld.Clear();
            texturesNew.Clear();
            ParseIgnored();

            texturesOld = LoadTextures(OldTexturesPath);
            texturesNew = LoadTextures(NewTexturesPath);
            FindSimilarTextures();
        }

        static public void ExportTextureList ()
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
            else return;

            //collect matches
            foreach (var texture in texturesOld)
            {
                if (texture.BestMatch != null)
                {
                    var keyName = Path.GetFileNameWithoutExtension(texture.Path);
                    var keyDir  = Path.GetFileName(Path.GetDirectoryName(texture.Path));
                    var key     = Path.Combine(keyDir, keyName);

                    var valName = Path.GetFileNameWithoutExtension(texture.BestMatch.Path);
                    var valDir  = Path.GetFileName(Path.GetDirectoryName(texture.BestMatch.Path));
                    var value   = Path.Combine(valDir, valName);

                    texturePaths.Add(key, value);
                }
            }
            
            //check matches exist
            if (texturePaths.Count == 0)
            {
                MessageBox.Show("No texture matches to save!");
                return;
            }

            //find offset
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

            //save
            try
            {
                File.WriteAllText(savePath, builder.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }

}