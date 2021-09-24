namespace Lipar.Entities.Domain.Organization.Enums
{
    public enum LoginResultTypeEnum
    {
        /// <summary>
        /// لاگین با موفقیت انجام شده
        /// </summary>
        Successful = 1,
        /// <summary>
        /// کاربر موجود نمی باشد
        /// </summary>
        UserNotExist,
        /// <summary>
        /// رمز عبور اشتباه است
        /// </summary>
        WrongPassword,
        /// <summary>
        /// حساب کاربری فعال نیست
        /// </summary>
        NotActive,
        /// <summary>
        /// حساب کاربر حذف شده است
        /// </summary>
        Deleted,
        /// <summary>
        /// حساب کاربری ثبت نام نشده است
        /// </summary>
        NotRegistered,
        /// <summary>
        /// حساب قفل می باشد
        /// </summary>
        LockedOut
    }
}
