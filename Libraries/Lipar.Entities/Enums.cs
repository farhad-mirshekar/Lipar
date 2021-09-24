namespace Lipar.Entities
{
    public enum CommentStatusType : byte
    {
        Unknown = 0,
        فعال = 1,
        غیر_فعال = 2
    }
    public enum UserType : byte
    {
        Unknown = 0,
        کاربر_درون_سازمان = 1,
        کاربر_بیرون_سازمان = 2
    }
    public enum PositionType : byte
    {
        Unknown = 0,
        محتوا_گذار = 1,
        مدیر_سایت = 100
    }
    public enum EnabledType : byte
    {
        Unknown = 0,
        /// <summary>
        /// فعال
        /// </summary>
        Active = 1,
        /// <summary>
        /// غیرفعال
        /// </summary>
        InActive = 2
    }
    public enum DepartmentType : byte
    {
        Unknown = 0,
        سامانه_اصلی = 1,
        واحد_فروش = 2,
        واحد_مالی = 3,
        واحد_انبار_و_لجستیک = 4
    }
    public enum CommandType : byte
    {
        Unknown = 0,
        App = 1,
        Mnu = 2,
        Pge = 3
    }
    public enum ViewStatusType : byte
    {
        Unknown = 0,
        /// <summary>
        /// فعال
        /// </summary>
        Active = 1,
        /// <summary>
        /// غیرفعال
        /// </summary>
        InActive = 2
    }
    public enum LoginResultType : byte
    {
        unknown = 0,
        Successful = 1,
        UserNotExist = 2,
        WrongPassword = 3,
        NotActive = 4,
        Deleted = 5,
        NotRegistered = 6,
        LockedOut = 7
    }
    public enum PasswordFormatType : byte
    {
        unknown = 0,
        Clear = 1,
        Hashed = 2,
        Encrypted = 3
    }
    public enum SettingServiceType : byte
    {
        SecuritySetting = 1
    }
    public enum LanguageCultureType : byte
    {
        Unknown = 0,
        English = 1,
        French = 2,
        German = 3,
        Turkish = 4,
        Spanish = 5,
        Persian = 6,
        Arabic = 7,
        Hindi = 8,
        Chinese = 9
    }
    public enum MediaType : byte
    {
        Unknown = 0,
        اصلی = 1,
        ثانویه = 2
    }
    public enum ForeignLinkType : byte
    {
        Unknown = 0,
        لینک_داخلی = 1,
        لینک_خارجی = 2
    }
    public enum NotifyType : byte
    {
        Unknown = 0,
        success = 1,
        warning = 2,
        error = 3
    }
    public enum ContactUsType : byte
    {
        Unknown = 0,
        ارتباط_با_ادمین = 1,
        مشکلات_سایت = 2
    }
    public enum DiscountType : byte
    {
        Unknown = 0,
        مبلغی = 1,
        در_صدی = 2
    }
    public enum AttributeControlType : byte
    {
        Unknown = 0,
        DropdownList = 1
    }
    public enum ProductSortingType : byte
    {
        Unknown = 0,
        NameAsc = 1,
        NameDesc = 2,
        CreationDateAsc = 3,
        CreationDateDesc = 4
    }
}
