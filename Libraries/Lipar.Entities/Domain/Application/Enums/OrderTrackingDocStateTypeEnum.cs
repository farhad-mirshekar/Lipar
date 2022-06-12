namespace Lipar.Entities.Domain.Application.Enums
{
    public enum OrderTrackingDocStateTypeEnum
    {
        /// <summary>
        /// ثبت اولیه 
        /// </summary>
        InitialRegistration = 1,

        /// <summary>
        /// بررسی مالی
        /// </summary>
        FinancialUnitReview,

        /// <summary>
        /// بررسی واحد انبار و لجستیک
        /// </summary>
        WarehouseUnitReview,

        /// <summary>
        /// ارسال محصول
        /// </summary>
        SendProduct,

        /// <summary>
        /// تحویل به مشتری
        /// </summary>
        DeliveryToCustomer
    }
}
