using FluentValidation.Attributes;
using FluentValidation.Results;
using PersonBook.Application.Shared;
using PersonBook.Infrastructure.Db;
using Serilog;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PersonBook.Application.Infrastructure
{
    public class CommandExecutor
    {
        private readonly PersonBookDbContext _db;
        private readonly UnitOfWork _unitOfWork;
        private readonly ApplicationContext _applicationContext;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public CommandExecutor(PersonBookDbContext db,
                               UnitOfWork unitOfWork,
                               IServiceProvider serviceProvider,
                               ApplicationContext applicationContext,
                               ILogger logger)
        {
            _db = db;
            _unitOfWork = unitOfWork;
            _serviceProvider = serviceProvider;
            _applicationContext = applicationContext;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task<CommandExecutionResult> ExecuteAsync(Command command)
        {
            try
            {
                var validationResult = Validate(command);
                if (!validationResult.IsValid)
                {
                    return new CommandExecutionResult
                    {
                        Success = false,
                        Error = new Error
                        {
                            Errors = validationResult.Errors.Select(error => error.ErrorMessage)
                        }
                    };
                }
                command.Resolve(_db, _unitOfWork, _serviceProvider, _applicationContext);

                return await command.ExecuteAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());

                return new CommandExecutionResult
                {
                    Success = false
                };
            }
        }

        public static ValidationResult Validate(Command execution)
        {
            var validatorAttribute = execution.GetType().GetCustomAttribute<ValidatorAttribute>(true);
            if (validatorAttribute != null)
            {
                var instance = (dynamic)Activator.CreateInstance(validatorAttribute.ValidatorType);
                var modelState = instance.Validate((dynamic)execution);
                return modelState;
            }

            return new ValidationResult();
        }
    }
}
