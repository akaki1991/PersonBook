namespace PersonBook.Domain.Shared
{
    public abstract class ValueObject : IThrowsDomainExeption
    {
        public void ThrowDomainException(string message)
        {
            throw new DomainException(message);
        }
    }
}
