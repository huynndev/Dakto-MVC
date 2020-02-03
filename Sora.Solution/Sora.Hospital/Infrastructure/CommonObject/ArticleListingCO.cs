using Sora.Services.ViewModels;
using System.Collections.Generic;

namespace Sora.Hospital.Infrastructure.CommonObjects
{
    public class ArticleList
    {
       public ArticleListingCO MainArticleList { set; get; }
        public ArticleListingCO EnglishArticleList { set; get; }
    }

    public class  ArticleListingCO
    {
        public int CurrentPage { get; set; }

        public List<ArticleViewModel> List { get; set; } = new List<ArticleViewModel>();
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public string Lang { get; set; }
        public string searchText { get; set; }
        
        public bool HasPreviousPage => CurrentPage > 0;

        public bool HasNextPage => CurrentPage + 1 < TotalPages;
    }
}
