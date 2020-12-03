namespace PersonBook.Domain.CityAggregate.ReadModels
{
    public class CityReadModel
    {
        public int Id { get; set; }

        public int AggregateRootId { get; set; }

        public string Name { get; set; }
    }
}
