using FluentValidation;
using FluentValidation.Attributes;
using PersonBook.Application.Infrastructure;
using PersonBook.Application.Shared;
using System.Threading.Tasks;

namespace PersonBook.Application.Commands.PersonConnectionCommands
{
    [Validator(typeof(DeletePersionRelationshipCommandValidator))]
    public class DeletePersionRelationshipCommand : Command
    {
        public int Id { get; set; }

        public async override Task<CommandExecutionResult> ExecuteAsync()
        {
            var personRelationship = await _personRelationshipRepository.OfIdAsync(Id);

            if (personRelationship == null)
                return await FailAsync(ErrorCode.NotFound);

            personRelationship.RaiseDeletedEvent();

            _personRelationshipRepository.Delete(personRelationship);

            await _unitOfWork.SaveAsync();

            return await OkAsync(DomainOperationResult.CreateEmpty());
        }
    }

    public class DeletePersionRelationshipCommandValidator : AbstractValidator<DeletePersionRelationshipCommand>
    {
        public DeletePersionRelationshipCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        }
    }
}
