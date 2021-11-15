using FluentAssertions;
using Lipar.Entities.Domain.Core.Enums;
using Lipar.Services.Notification;
using Lipar.Tests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Services.Tests.Notification
{
    [TestFixture]
    public class NotificationServiceTests : ServiceTest
    {
        #region Fields
        private INotificationService _notificationService;
        private Mock<IHttpContextAccessor> _httpContextAccessor;
        private Mock<ITempDataDictionaryFactory> _tempDataDictionaryFactory;
        private ITempDataDictionary _dataDictionary;
        #endregion

        [SetUp]
        public new void SetUp()
        {
            var httpContext = new Mock<HttpContext>();
            var tempDataProvider = new Mock<ITempDataProvider>();

            _dataDictionary = new TempDataDictionary(httpContext.Object, tempDataProvider.Object);

            _httpContextAccessor = new Mock<IHttpContextAccessor>();
            _tempDataDictionaryFactory = new Mock<ITempDataDictionaryFactory>();
            _tempDataDictionaryFactory.Setup(f => f.GetTempData(It.IsAny<HttpContext>())).Returns(_dataDictionary);
            _notificationService = new NotificationService(httpContextAccessor: _httpContextAccessor.Object, tempDataDictionaryFactory: _tempDataDictionaryFactory.Object);
        }

        private IList<NotifyData> DeserializedDataDictionary =>
            JsonConvert.DeserializeObject<IList<NotifyData>>(_dataDictionary[LiparNotificationDefaults.NotificationListKey].ToString());

        [Test]
        public void Can_Add_Notification()
        {
            _notificationService.SusscessNotification("success");
            _notificationService.WarningNotification("warrning");
            _notificationService.ErrorNotification("error");

            var messageList = DeserializedDataDictionary;

            messageList.Count.Should().Be(3);

            var succMsg = messageList
                .Where(m => m.NotifyType == NotifyTypeEnum.success)
                .First();
            succMsg.Message.Should().Be("success");

            var warnMsg = messageList
                .Where(m => m.NotifyType == NotifyTypeEnum.warning)
                .First();
            warnMsg.Message.Should().Be("warrning");

            var errMsg = messageList
                .Where(m => m.NotifyType == NotifyTypeEnum.error)
                .First();
            errMsg.Message.Should().Be("error");
        }
    }
}
