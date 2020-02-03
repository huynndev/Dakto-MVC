using Sora.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sora.Services.Abstractions
{
    public interface IArticleCategoryService
    {
        void CreateArticleCategory(ArticleCategoryViewModel post);

        void Update(ArticleCategoryViewModel post);

        void Delete(ArticleCategoryViewModel post);

        ArticleCategoryViewModel GetCategoryByID(int iObjectID);

        IEnumerable<ArticleCategoryViewModel> GetCategoryByParentID(int parentID);

        IEnumerable<ArticleCategoryViewModel> GetAllCategory();
    }
}
