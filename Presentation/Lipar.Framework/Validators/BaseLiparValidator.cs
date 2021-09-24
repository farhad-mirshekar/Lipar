using FluentValidation;

namespace Lipar.Web.Framework.Validators
{
    public abstract class BaseLiparValidator<T> : AbstractValidator<T> where T : class
    {
    }
}
