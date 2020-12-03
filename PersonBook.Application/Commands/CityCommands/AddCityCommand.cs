using FluentValidation;
using FluentValidation.Attributes;
using Microsoft.EntityFrameworkCore;
using PersonBook.Application.Infrastructure;
using PersonBook.Application.Shared;
using PersonBook.Domain.CityAggregate;
using PersonBook.Domain.CityAggregate.Repositories;
using System.Threading.Tasks;

namespace PersonBook.Application.Commands.CityCommands
{
    [Validator(typeof(AddCityCommandValidator))]
    public class AddCityCommand : Command
    {
        public string Name { get; set; }

        public async override Task<CommandExecutionResult> ExecuteAsync()
        {
            var cityRepository = GetService<ICityRepository>();

            var cityWithDuplicatename = await cityRepository.Query(x => x.Name == Name).FirstOrDefaultAsync();

            if (cityWithDuplicatename != null)
                return await FailAsync(ErrorCode.Denied, "City with this name already exists.");

            var city = new City(Name);

            await SaveAsync(city, cityRepository);

            return await OkAsync(DomainOperationResult.Create(city.Id));
        }
    }

    public class AddCityCommandValidator : AbstractValidator<AddCityCommand>
    {
        public AddCityCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
