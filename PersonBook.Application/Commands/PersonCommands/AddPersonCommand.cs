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
    [Validator(typeof(AddPersonCommandValidator))]
    public class AddPersonCommand : Command
    {
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
            if (await _personRepository.Query(x => x.PersonalNumber == PersonalNumber).AnyAsync())
                return await FailAsync(ErrorCode.Denied, $"User with personalId: {PersonalNumber} Already exists");

            var city = await _db.Set<City>().FirstOrDefaultAsync(x => x.Id == CityId);

            if (city == null)
                return await FailAsync(ErrorCode.Denied, $"City with Id: {CityId} does not exist exists");

            var phoneNumbers = await _personRepository.GetPhoneNumbersAsync(PhoneNumbers.Select(x => x.Number).ToList());

            if (phoneNumbers.Any())
                return await FailAsync(ErrorCode.Denied, $"Phone number: {phoneNumbers.First().Number} already taken");

            var photo = await _photoRepository.OfIdAsync(PhotoId);

            var personPhoto = new Domain.PersonAggregate.ValueObjects.Photo(photo.Url, photo.Width, photo.Height);

            var person = new Person(FirstName,
                                    LastName,
                                    Gender,
                                    PersonalNumber,
                                    BirthDate,
                                    city.Name,
                                    city.Id,
                                    PhoneNumbers.Select(x => x.ToDomainModel()).ToList(),
                                    personPhoto,
                                    photo.Id);

            await SaveAsync(person, _personRepository);

            return await OkAsync(DomainOperationResult.Create(person.Id));
        }
    }

    public class AddPersonCommandValidator : AbstractValidator<AddPersonCommand>
    {
        public AddPersonCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().Length(2, 50).SetValidator(new GeorgianLatinLettersValidation());
            RuleFor(x => x.LastName).NotEmpty().Length(2, 50).SetValidator(new GeorgianLatinLettersValidation());
            RuleFor(x => x.Gender).NotEmpty().IsInEnum();
            RuleFor(x => x.PersonalNumber).NotEmpty().Length(11, 11);
            RuleFor(x => x.BirthDate).LessThanOrEqualTo(DateTime.Now.AddYears(-18))
                                     .WithMessage("Person should be at least 18 years old");
            RuleFor(x => x.CityId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.PhotoId).NotEmpty().GreaterThan(0);
            RuleForEach(x => x.PhoneNumbers).SetValidator(x => new PhoneNumberDtoValidator());
        }
    }
}
