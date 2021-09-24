namespace Lipar.Services.Notification
{
   public interface INotificationService
    {
        /// <summary>
        /// show success notification
        /// </summary>
        /// <param name="message"></param>
        void SusscessNotification(string message);
        /// <summary>
        /// show warning notification
        /// </summary>
        /// <param name="message"></param>
        void WarningNotification(string message);
        /// <summary>
        /// show error notification
        /// </summary>
        /// <param name="message"></param>
        void ErrorNotification(string message);
    }
}
