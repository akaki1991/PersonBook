using PersonBook.Infrastructure.Db;
using System;
using System.Threading.Tasks;

namespace PersonBook.Application.Infrastructure
{
    public class QueryExecutor
    {
        private readonly PersonBookDbContext _db;
        private readonly UnitOfWork _unitOfWork;
        private readonly IServiceProvider _serviceProvider;
        private readonly ApplicationContext _applicationContext;

        public QueryExecutor(PersonBookDbContext db, UnitOfWork unitOfWork, IServiceProvider serviceProvider, ApplicationContext applicationContext)
        {
            _db = db;
            _unitOfWork = unitOfWork;
            _serviceProvider = serviceProvider;
            _applicationContext = applicationContext;
            _serviceProvider = serviceProvider;
        }

        public async Task<QueryExecutionResult<TResult>> ExecuteAsync<TQuery, TResult>(TQuery query)
            where TQuery : Query<TResult>
            where TResult : class
        {
            try
            {
                query.Resolve(_db, _unitOfWork, _serviceProvider, _applicationContext);

                return await query.ExecuteAsync();
            }
            catch (Exception ex)
            {
                ex.ToString();

                // TODO: Log Exception
                return new QueryExecutionResult<TResult>
                {
                    Success = false
                };
            }
        }
    }
}
