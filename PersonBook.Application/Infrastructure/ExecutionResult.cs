using PersonBook.Application.Shared;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PersonBook.Application.Infrastructure
{
    public class ExecutionResult
    {
        public bool Success { get; set; }

        public DomainOperationResult Data { get; set; }

        public Error Error { get; set; }

        [JsonIgnore]
        public ErrorCode ErrorCode { get; set; }
    }
}