using PersonBook.Application.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonBook.Application.Infrastructure
{
    public abstract class Query<TQueryResult> : ApplicationBase
        where TQueryResult : class
    {
        public abstract Task<QueryExecutionResult<TQueryResult>> ExecuteAsync();

        protected async Task<QueryExecutionResult<TQueryResult>> FailAsync(params string[] errorMessages)
        {
            var result = new QueryExecutionResult<TQueryResult>
            {
                Success = false
            };

            if (!errorMessages.Any())
            {
                result.Error.Errors = errorMessages;
            }

            return await Task.FromResult(result);
        }

        protected async Task<QueryExecutionResult<TQueryResult>> FailAsync(ErrorCode errorCode)
        {
            var result = new QueryExecutionResult<TQueryResult>
            {
                Success = false,
                ErrorCode = errorCode
            };

            return await Task.FromResult(result);
        }

        protected async Task<QueryExecutionResult<TQueryResult>> OkAsync(TQueryResult data)
        {
            var result = new QueryExecutionResult<TQueryResult>
            {
                Data = data,
                Success = true
            };

            return await Task.FromResult(result);
        }
    }
}
