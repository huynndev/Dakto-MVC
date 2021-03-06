﻿using Sora.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sora.Services.Abstractions
{
    public interface IArticleService
    {
        void CreateArticle(ArticleViewModel post);

        void Update(ArticleViewModel post);

        void Delete(ArticleViewModel post);

        ArticleViewModel GetArticleByID(int articleID);

        IEnumerable<ArticleViewModel> GetArticleByCategoryID(int categoryID, int page, int pageSize, string sortOption, out int totalRow, string search = null);

    }
}
