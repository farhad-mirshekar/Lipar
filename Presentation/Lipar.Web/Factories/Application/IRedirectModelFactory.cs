using Lipar.Web.Models.Application;
using Melli.Portal.Model;

namespace Lipar.Web.Factories.Application
{
    /// <summary>
    /// redirect all bank service
    /// </summary>
    public interface IRedirectModelFactory
    {
        RedirectModel PrepareMelli(PurchaseResult purchaseResult);
    }
}
