using FluentValidation;
using FluentValidation.Attributes;
using Microsoft.EntityFrameworkCore;
using PersonBook.Application.Infrastructure;
using PersonBook.Application.Shared;
using PersonBook.Domain.PersonRelationAggregate;
using System.Linq;
using System.Threading.Tasks;

namespace PersonBook.Application.Commands.PersonConnectionCommands
{
    [Validator(typeof(AddPersionRelationshipCommandValidator))]
    public class AddPersionRelationshipCommand : Command
    {
        public int FirstPersonId { get; set; }

        public int SecondPersonId { get; set; }

        public PersonRelationshipType PersonRelationshipType { get; set; }

        public async override Task<CommandExecutionResult> ExecuteAsync()
        {
            var realatedPersons = await _personRepository
                .Query(x => x.Id == FirstPersonId || x.Id == SecondPersonId)
                .ToListAsync();

            if (!realatedPersons.Any(x => x.Id != FirstPersonId))
                return await FailAsync(ErrorCode.NotFound, $"Person with id {FirstPersonId} does not exist");

            if (!realatedPersons.Any(x => x.Id != SecondPersonId))
                return await FailAsync(ErrorCode.NotFound, $"Person with id {SecondPersonId} does not exist");

            var personRealationExists = await _personRelationshipRepository
                .Query(x => (x.FirstPersonId == FirstPersonId && x.SecondPersonId == SecondPersonId)
                            || (x.FirstPersonId == SecondPersonId && x.SecondPersonId == FirstPersonId))
                .AnyAsync();

            if (personRealationExists)
                return await FailAsync(ErrorCode.Denied, $"Relation with Ids {FirstPersonId} and {SecondPersonId} already exists.");

            var personRelationship = new PersonRelationship(PersonRelationshipType, FirstPersonId, SecondPersonId);

            await SaveAsync(personRelationship, _personRelationshipRepository);

            return await OkAsync(DomainOperationResult.Create(personRelationship.Id));
        }
    }

    public class AddPersionRelationshipCommandValidator : AbstractValidator<AddPersionRelationshipCommand>
    {
        public AddPersionRelationshipCommandValidator()
        {
            RuleFor(x => x.FirstPersonId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.SecondPersonId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.PersonRelationshipType).IsInEnum();
        }
    }
}
