using MapComparer.Interface;
using MapComparer.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MapComparer
{
    enum Pages
    {
        StartPage,
        ComparisonPage,
    }
    class PageManager : BindableObject
    {
        private Page currentPage;
        public Page CurrentPage 
        { 
            get => currentPage;
            private set { currentPage = value; OnPropertyChanged(); }
        }

        public void SetCurrentPage (Pages page)
        {
            switch (page)
            {
                case Pages.StartPage:
                    CurrentPage = new StartPage();
                    break;
                case Pages.ComparisonPage:
                    CurrentPage = new ComparisonPage();
                    break;
                default:
                    break;
            }
        }
    }
}