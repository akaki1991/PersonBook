using PersonBook.Application.Shared;

namespace PersonBook.Application.Infrastructure
{
    public class QueryExecutionResult<T>
    {
        public QueryExecutionResult()
        {
            Error = new Error();
        }

        public bool Success { get; set; }

        public T Data { get; set; }

        public ErrorCode ErrorCode { get; set; }

        public Error Error { get; set; }
    }
}