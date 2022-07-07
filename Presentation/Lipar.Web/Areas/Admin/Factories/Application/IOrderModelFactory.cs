using Lipar.ViewModels.Application;
using System;

namespace Lipar.Web.Areas.Admin.Factories.Application
{
    public interface IOrderModelFactory
    {
        OrderViewModel PrepareInvoiceDetail(Guid orderId);
    }
}
