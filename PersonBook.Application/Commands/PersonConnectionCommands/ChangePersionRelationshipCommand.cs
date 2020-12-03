using FluentValidation;
using FluentValidation.Attributes;
using Microsoft.EntityFrameworkCore;
using PersonBook.Application.Infrastructure;
using PersonBook.Application.Shared;
using PersonBook.Domain.PersonRelationAggregate;
using PersonBook.Domain.PersonRelationAggregate.Repositories;
using System.Threading.Tasks;

namespace PersonBook.Application.Commands.PersonConnectionCommands
{
    [Validator(typeof(ChangePersionRelationshipCommandValidator))]
    public class ChangePersionRelationshipCommand : Command
    {
        public int FirstPersonId { get; set; }

        public int SecondPersonId { get; set; }

        public PersonRelationshipType PersonRelationshipType { get; set; }

        public async override Task<CommandExecutionResult> ExecuteAsync()
        {
            var personRealationship = await _personRelationshipRepository
                .Query(x => (x.FirstPersonId == FirstPersonId && x.SecondPersonId == SecondPersonId)
                            || (x.FirstPersonId == SecondPersonId && x.SecondPersonId == FirstPersonId))
                .FirstOrDefaultAsync();

            if (personRealationship == null)
                return await FailAsync(ErrorCode.NotFound);

            personRealationship.ChangePersonRelationshipType(PersonRelationshipType);

            await SaveAsync(personRealationship, _personRelationshipRepository);

            return await OkAsync(DomainOperationResult.Create(personRealationship.Id));
        }
    }

    public class ChangePersionRelationshipCommandValidator : AbstractValidator<ChangePersionRelationshipCommand>
    {
        public ChangePersionRelationshipCommandValidator()
        {
            RuleFor(x => x.PersonRelationshipType).IsInEnum();
            RuleFor(x => x.FirstPersonId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.SecondPersonId).NotEmpty().GreaterThan(0);
        }
    }
}
