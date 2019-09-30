using System.Collections.Generic;
using System.Windows.Controls;
using MapComparer.Interface;

namespace MapComparer.Model
{
    public enum Pages
    {
        Main,
        Start,
    }

    static class PageManager
    {
        private static Dictionary<Pages, Page>  pages;
        public static Frame frame;

        static PageManager ()
        {
            pages = new Dictionary<Pages, Page>()
            {
                { Pages.Main,   new ComparisonPage()    },
                { Pages.Start,  new StartPage()         },
            };
        }

        public static void SetFramePage (Pages page)
        {
            frame.Content = pages[page];
        }
    }
}