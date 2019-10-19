using MapComparer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mapcompare.Utilites
{
    class TexturePathValidator
    {
        //"act, andy, artifact, detail, glow, map, fx, pda, ed, hud, intro, icon, ui, internal, lights, pfx, terrain, sky, wm, water, wpn";
        //"sunmask.dds, ui_icons_npc_old.dds, water_sbumpvolume.dds, bump, bump#";
        
        private static List<string> ignoredSubDirectories  = new List<string> { "TEST", "MASK", "DASDAS" };
        private static List<string> ignoredPostfixes       = new List<string> { "_BUMP", "_DET" };

        public static bool Validate (string texturesDirPath, string texturePath)
        {
            return texturePath.Contains("_bump");


            var subPath = texturePath.Replace(texturesDirPath, string.Empty);
            
            var substrings = subPath.Split('\\');
            if (substrings.Length > 0)
            {
                var subDirectoryPath = substrings[0].ToUpper();
                return ignoredSubDirectories.Contains(subDirectoryPath);
            }

            return false;
        }
    }
}
