using Lipar.Core.Common;
using Lipar.Entities.Domain.Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Lipar.Services.Notification
{
    public class NotificationService : INotificationService
    {
        #region Fields
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;
        #endregion

        #region Ctor
        public NotificationService(IHttpContextAccessor httpContextAccessor
                                 , ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
        }
        #endregion

        #region Method
        public void ErrorNotification(string message)
        => PrepareNotification(NotifyTypeEnum.error, message);

        public void SusscessNotification(string message)
        => PrepareNotification(NotifyTypeEnum.success, message);

        public void WarningNotification(string message)
        => PrepareNotification(NotifyTypeEnum.warning, message);
        #endregion

        #region Utilities
        protected void PrepareNotification(NotifyTypeEnum notifyType , string Message)
        {
            var context = _httpContextAccessor.HttpContext;
            var tempData = _tempDataDictionaryFactory.GetTempData(context);

            //Messages have stored in a serialized list
            var messages = tempData.ContainsKey(LiparNotificationDefaults.NotificationListKey)
                ? CommonHelper.DeserializeObject<IList<NotifyData>>(tempData[LiparNotificationDefaults.NotificationListKey].ToString())
                : new List<NotifyData>();

            messages.Add(new NotifyData
            {
                Message = Message,
                NotifyType = notifyType,
            });

            tempData[LiparNotificationDefaults.NotificationListKey] = JsonConvert.SerializeObject(messages);
        }
        #endregion
    }
}
