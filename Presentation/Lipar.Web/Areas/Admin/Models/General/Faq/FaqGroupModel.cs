namespace Lipar.Web.Areas.Admin.Models.General
{
    public class FaqGroupModel : BaseEntityModel
    {
        #region Ctor
        public FaqGroupModel()
        {
            FaqSearchModel = new FaqSearchModel();
        }
        #endregion

        #region Fields
        public string Name { get; set; }
        public FaqSearchModel FaqSearchModel { get; set; }
        #endregion
    }
}
