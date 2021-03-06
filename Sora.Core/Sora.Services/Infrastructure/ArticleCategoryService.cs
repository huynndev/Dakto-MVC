﻿using Sora.Common;
using Sora.EFCore.Infrastructure;
using Sora.EFCore.Repositories;
using Sora.Entites.IC;
using Sora.Services.Abstractions;
using Sora.Services.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Sora.Services.Infrastructure
{
    public class ArticleCategoryService : IArticleCategoryService
    {
        private IArticleCategoryRepository _articleCategoryRepository;
        private IUnitOfWork _unitOfWork;

        public ArticleCategoryService(IArticleCategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            this._articleCategoryRepository = categoryRepository;
            this._unitOfWork = unitOfWork;
        }

        public ArticleCategoryViewModel GetCategoryByID(int iObjectID)
        {
            ICArticleCategory category = _articleCategoryRepository.GetSingleById(iObjectID);
            return ToArticleCategoryViewModel(category);
        }

        public IEnumerable<ArticleCategoryViewModel> GetAllCategory()
        {
            try
            {
                var categories = _articleCategoryRepository.GetAll().Include(x => x.ICArticles).ToList();
                return categories.Select(o => ToArticleCategoryViewModel(o));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public void Add(ArticleCategoryViewModel category)
        {
            _articleCategoryRepository.Add(ToArticleCategory(category));
        }

        public void CreateArticleCategory(ArticleCategoryViewModel obj)
        {
            Add(obj);
            Save();
        }

        public void Update(ArticleCategoryViewModel obj)
        {
            _articleCategoryRepository.Update(ToArticleCategory(obj));
            Save();
        }

        public void Delete(ArticleCategoryViewModel obj)
        {
            ICArticleCategory category = ToArticleCategory(obj);
            _articleCategoryRepository.DeleteSoft(category);
            Save();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public ArticleCategoryViewModel ToArticleCategoryViewModel(ICArticleCategory category)
        {
            if (category == null)
                return null;

            return new ArticleCategoryViewModel()
            {
                ICArticleCategoryID = category.ICArticleCategoryID,
                ICArticleCategoryName = category.ICArticleCategoryName,
                ICArticleCategoryNo = category.ICArticleCategoryNo,
                ICArticleCategoryDesc = category.ICArticleCategoryDesc,
                ICArticleCategorySortOrder = category.ICArticleCategorySortOrder,
                ICArticleCategoryIsShowMenu = category.ICArticleCategoryIsShowMenu ?? false,
                ICArticleCategoryParentID = category.ICArticleCategoryParentID,
                CountICArticle = category.ICArticles.Count(x => x.AAStatus == AudiableStatus.Alive),
                AACreatedDate = category.AACreatedDate,
                AAUpdatedDate = category.AAUpdatedDate
            };
        }

        public ICArticleCategory ToArticleCategory(ArticleCategoryViewModel model)
        {
            if (model == null)
                return null;

            return new ICArticleCategory()
            {
                ICArticleCategoryID = model.ICArticleCategoryID,
                ICArticleCategoryName = model.ICArticleCategoryName,
                ICArticleCategoryNo = model.ICArticleCategoryNo,
                ICArticleCategoryDesc = model.ICArticleCategoryDesc,
                ICArticleCategorySortOrder = model.ICArticleCategorySortOrder,
                ICArticleCategoryIsShowMenu = model.ICArticleCategoryIsShowMenu,
                ICArticleCategoryParentID = model.ICArticleCategoryParentID
            };
        }

        public IEnumerable<ArticleCategoryViewModel> GetCategoryByParentID(int parentID)
        {
            var categories = _articleCategoryRepository.GetMulti(o => o.ICArticleCategoryParentID == parentID);
            return categories.Select(o => ToArticleCategoryViewModel(o));
        }
    }
}
