using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PersonBook.Api.Infrastructure
{
    public class LowercaseDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var openAPiPaths = new OpenApiPaths();

            foreach (var item in swaggerDoc.Paths)
            {
                var key = item.Key.ToLower();

                openAPiPaths.Add(key, item.Value);
            }

            swaggerDoc.Paths = openAPiPaths;
        }
    }
}
