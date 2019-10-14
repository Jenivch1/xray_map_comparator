using MapComparer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mapcompare.Model.Utilites
{
    class PathValidator
    {
        private List<string> ignoredSubDirectories  = new List<string> { "TEST", "MASK", "DASDAS" };
        private List<string> ignoredPostfixes       = new List<string> { "_BUMP", "_DET" };

        public bool ValidateTexturePath (string directoryPath, string texturePath)
        {
            var subPath = texturePath.Replace(directoryPath, string.Empty);
            
            var substrings = subPath.Split('\\');
            if (substrings.Length > 0)
            {
                var subDirectoryPath = substrings[0].ToUpper();
                return ignoredSubDirectories.Contains(subDirectoryPath);
            }

            return false;

        }

        public bool ValidateTextureDirectoryPath(string directoryPath)
        {
            // if has no extension
            // if

            return true;
        }
    }
}
