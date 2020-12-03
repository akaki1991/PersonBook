using PersonBook.Domain.CityAggregate.Events;
using PersonBook.Domain.CityAggregate.ReadModels;
using PersonBook.Infrastructure.Db;
using PersonBook.Infrastructure.EvnetDispatching;
using System.Linq;

namespace PersonBook.Application.EventHandlers
{
    public class CityEventHandlers : IHandleEvent<CityPlacedEvent>,
        IHandleEvent<CityDeletedEvent>
    {
        public void Handle(CityPlacedEvent @event, PersonBookDbContext db)
        {
            var cityReadModel = new CityReadModel
            {
                AggregateRootId = @event.City.Id,
                Name = @event.City.Name,
            };

            db.Set<CityReadModel>().Add(cityReadModel);
        }

        public void Handle(CityDeletedEvent @event, PersonBookDbContext db)
        {
            var cityReadModel = db.Set<CityReadModel>().FirstOrDefault(x => x.AggregateRootId == @event.AggregateRootId);

            if (cityReadModel != null)
            {
                db.Set<CityReadModel>().Remove(cityReadModel);
            }
        }
    }
}
