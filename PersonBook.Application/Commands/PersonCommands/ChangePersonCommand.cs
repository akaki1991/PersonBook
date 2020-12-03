using FluentValidation;
using FluentValidation.Attributes;
using Microsoft.EntityFrameworkCore;
using PersonBook.Application.Commands.PersonCommands.Models;
using PersonBook.Application.Infrastructure;
using PersonBook.Application.Shared;
using PersonBook.Application.Shared.Helpers;
using PersonBook.Domain.CityAggregate;
using PersonBook.Domain.PersonAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonBook.Application.Commands.PersonCommands
{
    [Validator(typeof(ChangePersonCommandValidator))]
    public class ChangePersonCommand : Command
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PersonalNumber { get; set; }

        public DateTimeOffset BirthDate { get; set; }

        public int CityId { get; set; }

        public Gender Gender { get; set; }

        public int PhotoId { get; set; }

        public IEnumerable<PhoneNumberDto> PhoneNumbers { get; set; }

        public async override Task<CommandExecutionResult> ExecuteAsync()
        {
            var person = await _personRepository.OfIdAsync(Id);

            if (person == null)
                return await FailAsync(ErrorCode.NotFound);

            var city = await _db.Set<City>().FirstOrDefaultAsync(x => x.Id == CityId);

            if (city == null)
                return await FailAsync(ErrorCode.Denied, $"User with personalId: {PersonalNumber} Already exists");

            var phoneNumbers = await _personRepository.GetPhoneNumbersAsync(PhoneNumbers.Select(x => x.Number).ToList());

            if (phoneNumbers.Any(x => x.PersonId != person.Id))
                return await FailAsync(ErrorCode.Denied, $"Phone number: {phoneNumbers.First().Number} already taken");

            var photo = await _photoRepository.OfIdAsync(PhotoId);

            var personPhoto = new Domain.PersonAggregate.ValueObjects.Photo(photo.Url, photo.Width, photo.Height);

            person.ChangeDetails(FirstName,
                                 LastName,
                                 Gender,
                                 PersonalNumber,
                                 BirthDate,
                                 city.Name,
                                 city.Id,
                                 phoneNumbers.ToList(),
                                 personPhoto,
                                 photo.Id);

            await SaveAsync(person, _personRepository);

            return await OkAsync(DomainOperationResult.Create(person.Id));
        }
    }

    public class ChangePersonCommandValidator : AbstractValidator<ChangePersonCommand>
    {
        public ChangePersonCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty().Length(2, 50).SetValidator(new GeorgianLatinLettersValidation());
            RuleFor(x => x.LastName).NotEmpty().Length(2, 50).SetValidator(new GeorgianLatinLettersValidation());
            RuleFor(x => x.Gender).NotEmpty().IsInEnum();
            RuleFor(x => x.PersonalNumber).NotEmpty().Length(11, 11);
            RuleFor(x => x.BirthDate).LessThanOrEqualTo(DateTime.Now.AddYears(-18))
                                     .WithMessage("Person should be at least 18 years old");
            RuleFor(x => x.CityId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.PhotoId).NotEmpty().GreaterThan(0);
        }
    }
}
