using FluentValidation;

namespace PersonBook.Application.Shared.Helpers
{
    public static class ValidationExtension
    {
        public static IRuleBuilderOptions<T, TProperty> WithErrorCode<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, ErrorCode errorCode)
        {
            return rule.WithErrorCode(((int)errorCode).ToString());
        }
    }
}
