
using Lipar.Entities.Domain.Core.Enums;

namespace Lipar.Services.Notification
{
    public struct NotifyData
    {
        public NotifyTypeEnum NotifyType { get; set; }
        public string Message { get; set; }
    }
}
