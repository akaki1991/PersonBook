using FluentValidation;
using FluentValidation.Attributes;
using PersonBook.Application.Infrastructure;
using PersonBook.Application.Shared;
using System.Threading.Tasks;

namespace PersonBook.Application.Commands.PersonCommands
{
    [Validator(typeof(DeletePersonCommandValidator))]
    public class DeletePersonCommand : Command
    {
        public int Id { get; set; }

        public async override Task<CommandExecutionResult> ExecuteAsync()
        {
            var person = await _personRepository.OfIdAsync(Id);

            if (person == null)
                return await FailAsync(ErrorCode.NotFound);

            person.RaiseDeletedEvent();

            _personRepository.Delete(person);

            await _unitOfWork.SaveAsync();

            return await OkAsync(DomainOperationResult.CreateEmpty());
        }
    }

    public class DeletePersonCommandValidator : AbstractValidator<DeletePersonCommand>
    {
        public DeletePersonCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
