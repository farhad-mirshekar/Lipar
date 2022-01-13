using Lipar.Core;
using System;
using System.Collections.Generic;

namespace Lipar.Web.Framework.Models
{
    public static class ModelExtensions
    {
        public static TListModel PrepareToGrid<TListModel, TModel, TObject>(this TListModel listModel,
         BaseSearchModel searchModel, IPagedList<TObject> objectList, Func<IEnumerable<TModel>> dataFillFunction)
         where TListModel : BasePagedListModel<TModel>
         where TModel : BaseLiparModel
        {
            if (listModel == null)
                throw new ArgumentNullException(nameof(listModel));

            listModel.Data = dataFillFunction?.Invoke();
            listModel.Draw = searchModel?.Draw;
            listModel.RecordsTotal = objectList?.TotalCount ?? 0;
            listModel.RecordsFiltered = objectList?.TotalCount ?? 0;

            return listModel;
        }
    }
}
