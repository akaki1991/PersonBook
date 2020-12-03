using FluentValidation;
using FluentValidation.Attributes;
using PersonBook.Application.Infrastructure;
using PersonBook.Application.Shared;
using PersonBook.Domain.CityAggregate.Repositories;
using System.Threading.Tasks;

namespace PersonBook.Application.Commands.CityCommands
{
    [Validator(typeof(DeleteCityCommandValidator))]
    public class DeleteCityCommand : Command
    {
        public int Id { get; set; }

        public async override Task<CommandExecutionResult> ExecuteAsync()
        {
            var cityRepository = GetService<ICityRepository>();
            var city = await cityRepository.OfIdAsync(Id);

            if (city == null)
                return await FailAsync(ErrorCode.NotFound);

            cityRepository.Delete(city);

            city.RaiseDeleteEvent();

            await _unitOfWork.SaveAsync();

            return await OkAsync(DomainOperationResult.CreateEmpty());
        }
    }

    public class DeleteCityCommandValidator : AbstractValidator<DeleteCityCommand>
    {
        public DeleteCityCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
