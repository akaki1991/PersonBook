using PersonBook.Domain.Shared;
using System;

namespace PersonBook.Domain.PhotoAggregate
{
    public class Photo : AggregateRoot
    {
        public Photo(string url, int width, int height)
        {
            Url = url;
            Width = width;
            Height = height;

            CreateDate = DateTimeOffset.Now.ToUniversalTime();
        }

        public string Url { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }
    }
}
