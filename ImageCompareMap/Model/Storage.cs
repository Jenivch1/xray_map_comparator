using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapComparer.Model
{

    static class Storage
    {

        //TODO: Move to tex mgr.
        public static string    OldTexturesPath     = @"C:\Users\User\Downloads\! stcs textures original\textures\crete";
        public static string    NewTexturesPath     = @"C:\Users\User\Downloads\! stcs textures original\textures\crete";
        public static string    IgnoredSubfolders   = @"act, andy, artifact, detail, glow, map, fx, pda, ed, hud, intro, icon, ui, internal, lights, pfx, terrain, sky, wm, water, wpn";
        public static string    IgnoredTextures     = @"sunmask.dds, ui_icons_npc_old.dds, water_sbumpvolume.dds, bump, bump#";

        public static short     PreviewResolution   = 128;
        public static byte      MatchThreshold      = 5;

        public static void GetOldPath ()
        {
            OldTexturesPath = PickFolder();
        }

        public static void GetNewPath ()
        {
            NewTexturesPath = PickFolder();
        }

        private static string PickFolder ()
        {
            return (new System.Windows.Forms.FolderBrowserDialog()).SelectedPath;
        }

    }

}