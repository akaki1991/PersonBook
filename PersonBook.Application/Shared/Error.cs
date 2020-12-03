using System.Collections.Generic;

namespace PersonBook.Application.Shared
{
    public class Error
    {
        public Error()
        {
        }

        public IEnumerable<string> Errors { get; set; }
    }
}
