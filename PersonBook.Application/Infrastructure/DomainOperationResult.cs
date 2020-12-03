namespace PersonBook.Application.Infrastructure
{
    public class DomainOperationResult
    {
        public DomainOperationResult()
        {
        }

        public DomainOperationResult(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public static DomainOperationResult Create(int id) => new DomainOperationResult(id);

        public static DomainOperationResult CreateEmpty() => new DomainOperationResult();
    }
}