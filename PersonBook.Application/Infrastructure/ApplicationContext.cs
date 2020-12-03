using Microsoft.AspNetCore.Http;

namespace PersonBook.Application.Infrastructure
{
    public class ApplicationContext
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ApplicationContext(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
    }
}