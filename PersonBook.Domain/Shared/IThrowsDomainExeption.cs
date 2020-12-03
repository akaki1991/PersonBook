namespace PersonBook.Domain.Shared
{
    public interface IThrowsDomainExeption
    {
        void ThrowDomainException(string message);
    }
}
