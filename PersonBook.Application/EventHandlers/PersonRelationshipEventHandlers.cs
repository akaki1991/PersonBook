using PersonBook.Domain.PersonRelationAggregate.Events;
using PersonBook.Domain.PersonRelationAggregate.ReaedModels;
using PersonBook.Infrastructure.Db;
using PersonBook.Infrastructure.EvnetDispatching;
using System.Linq;

namespace PersonBook.Application.EventHandlers
{
    public class PersonRelationshipEventHandlers : IHandleEvent<PersonRelationshipPlacedEvent>,
        IHandleEvent<PersonRelationshipChangedEvent>,
        IHandleEvent<PersonRelationshipDeletedEvent>
    {
        public void Handle(PersonRelationshipPlacedEvent @event, PersonBookDbContext db)
        {
            var personRelationshipReadModel = new PersonRelationshipReadModel
            {
                AggregateRootId = @event.PersonRelationship.Id,
                FirstPersonId = @event.PersonRelationship.FirstPersonId,
                SecondPersonId = @event.PersonRelationship.SecondPersonId,
                CreateDate = @event.PersonRelationship.CreateDate,
                LastChangeDate = @event.PersonRelationship.LastChangeDate,
                PersonRelationshipType = @event.PersonRelationship.PersonRelationshipType
            };

            db.Set<PersonRelationshipReadModel>().Add(personRelationshipReadModel);
        }

        public void Handle(PersonRelationshipChangedEvent @event, PersonBookDbContext db)
        {
            var personRelationshipReadModel = db.Set<PersonRelationshipReadModel>()
                .FirstOrDefault(x => x.AggregateRootId == @event.PersonRelationship.Id);

            if (personRelationshipReadModel != null)
            {
                personRelationshipReadModel.FirstPersonId = @event.PersonRelationship.FirstPersonId;
                personRelationshipReadModel.SecondPersonId = @event.PersonRelationship.SecondPersonId;
                personRelationshipReadModel.PersonRelationshipType = @event.PersonRelationship.PersonRelationshipType;
                personRelationshipReadModel.CreateDate = @event.PersonRelationship.CreateDate;
                personRelationshipReadModel.LastChangeDate = @event.PersonRelationship.LastChangeDate;

                db.Set<PersonRelationshipReadModel>().Update(personRelationshipReadModel);
            }
        }

        public void Handle(PersonRelationshipDeletedEvent @event, PersonBookDbContext db)
        {
            var personRelationshipReadModel = db.Set<PersonRelationshipReadModel>()
               .FirstOrDefault(x => x.AggregateRootId == @event.AggregateRootId);

            if (personRelationshipReadModel != null)
            {
                db.Set<PersonRelationshipReadModel>().Remove(personRelationshipReadModel);
            }
        }
    }
}
