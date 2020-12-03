namespace PersonBook.Domain.Shared
{
    public abstract class Entity : IThrowsDomainExeption
    {
        public int Id { get; protected set; }

        public void ThrowDomainException(string message)
        {
            throw new DomainException(message);
        }
    }
}
