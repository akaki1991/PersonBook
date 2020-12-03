using PersonBook.Domain.Shared;

namespace PersonBook.Domain.PersonAggregate.ValueObjects
{
    public class Photo : ValueObject
    {
        public Photo(string url, int width, int height)
        {
            Url = url;
            Width = width;
            Height = height;
        }

        public string Url { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }
    }
}
