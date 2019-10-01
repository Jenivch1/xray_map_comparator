using Imaging.DDSReader;
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
    class TextureManager : BindableObject
    {
        private static TextureManager           instance;
        private ObservableCollection<Texture>   texturesNew;
        private ObservableCollection<Texture>   texturesOld;
        private string                          oldTexturesPath;
        private string                          newTexturesPath;

        //fix
        public string        IgnoredSubfolders  { get; set; }
        public string        IgnoredTextures    { get; set; }
        private List<string> ignoredFolders     { get; set; }

        public static TextureManager Instance
        {
            get
            {
                if (instance == null) { instance = new TextureManager(); }
                return instance;
            }
            private set => instance = value;
        }

        public string NewTexturesPath
        {
            get => newTexturesPath;
            set { newTexturesPath = value; OnPropertyChanged(); }
        }

        public string OldTexturesPath
        {
            get => oldTexturesPath;
            set { oldTexturesPath = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Texture> TexturesOld
        {
            get => texturesOld;
            set { texturesOld = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Texture> TexturesNew
        {
            get => texturesNew;
            set { texturesNew = value; OnPropertyChanged(); }
        }

        private TextureManager()
        {
#if DEBUG
            //TODO: Remove
            OldTexturesPath = @"C:\Users\User\Downloads\textures\textures_soc\crete";
            NewTexturesPath = @"C:\Users\User\Downloads\textures\textures_cs\crete";
            IgnoredSubfolders = @"act, andy, artifact, detail, glow, map, fx, pda, ed, hud, intro, icon, ui, internal, lights, pfx, terrain, sky, wm, water, wpn";
            IgnoredTextures = @"sunmask.dds, ui_icons_npc_old.dds, water_sbumpvolume.dds, bump, bump#";
#endif
            ignoredFolders = new List<string>();
            TexturesOld = new ObservableCollection<Texture>();
            TexturesNew = new ObservableCollection<Texture>();
        }

        private void ParseIgnored()
        {
            var temp = IgnoredSubfolders
                        .ToLower()
                        .Replace(" ", "")
                        .Replace("\t", "")
                        .Split(',');
            ignoredFolders = new List<string>(temp);
        }

        private bool IsIgnored(string path)
        {
            var pathDirectory = Path.GetFileName(Path.GetDirectoryName(path.ToLower()));
            return ignoredFolders.Contains(pathDirectory);
        }

        private ObservableCollection<Texture> LoadTextures(string directoryPath)
        {
            string[] paths = Directory.GetFiles(directoryPath, "*.dds", SearchOption.AllDirectories);
            var textures = new ObservableCollection<Texture>();

            foreach (var file in paths)
            {
                if (!IsIgnored(file))
                {
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

        public void ProcessTextures()
        {
            TexturesOld.Clear();
            TexturesNew.Clear();
            ParseIgnored();

            TexturesOld = LoadTextures(OldTexturesPath);
            TexturesNew = LoadTextures(NewTexturesPath);
            FindSimilarTextures();
            //MessageBox.Show(TexturesOld.Count.ToString());
        }

        public void ExportTextureList()
        {
            var savePath = string.Empty;
            var texturePaths = new Dictionary<string, string>();
            var align = 10;
            var builder = new StringBuilder("[new_texture_paths]");

            //pick folder
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                savePath = dialog.SelectedPath + @"\new_texture_paths.ini";
            }
            else return;

            //collect matches
            foreach (var texture in TexturesOld)
            {
                if (texture.BestMatch != null)
                {
                    var keyName = Path.GetFileNameWithoutExtension(texture.Path);
                    var keyDir = Path.GetFileName(Path.GetDirectoryName(texture.Path));
                    var key = Path.Combine(keyDir, keyName);

                    var valName = Path.GetFileNameWithoutExtension(texture.BestMatch.Path);
                    var valDir = Path.GetFileName(Path.GetDirectoryName(texture.BestMatch.Path));
                    var value = Path.Combine(valDir, valName);

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