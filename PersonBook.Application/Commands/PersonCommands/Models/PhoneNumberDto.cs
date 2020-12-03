using FluentValidation;
using PersonBook.Domain.PersonAggregate;

namespace PersonBook.Application.Commands.PersonCommands.Models
{
    public class PhoneNumberDto
    {
        public string Number { get; set; }

        public PhoneNumberType Type { get; set; }

        public PhoneNumber ToDomainModel()
        {
            return new PhoneNumber(Number, Type);
        }
    }

    public class PhoneNumberDtoValidator : AbstractValidator<PhoneNumberDto>
    {
        public PhoneNumberDtoValidator()
        {
            RuleFor(x => x.Number).NotEmpty().Length(4, 50);
            RuleFor(x => x.Type).IsInEnum();
        }
    }
}
