using System.Collections.Generic;
using System.Windows.Controls;
using MapComparer.Interface;

namespace MapComparer.Model
{
    static class PageFactory
    {
        public static Page CreateStartPage ()
        {
            var page = new StartPage();
            return page;
        }

        public static Page CreateComparisonPage()
        {
            var page = new ComparisonPage();
            return page;
        }
    }
}