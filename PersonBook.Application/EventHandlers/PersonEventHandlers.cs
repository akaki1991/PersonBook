using PersonBook.Domain.PersonAggregate;
using PersonBook.Domain.PersonAggregate.Events;
using PersonBook.Domain.PersonAggregate.ReadModels;
using PersonBook.Infrastructure.Db;
using PersonBook.Infrastructure.EvnetDispatching;
using System.Linq;

namespace PersonBook.Application.EventHandlers
{
    public class PersonEventHandlers : IHandleEvent<PersonAddedEvent>,
        IHandleEvent<PersonChangedEvent>,
        IHandleEvent<PersonDeletedEvent>

    {
        public void Handle(PersonAddedEvent @event, PersonBookDbContext db)
        {
            var dto = new PersonReadModel
            {
                AggregateRootId = @event.Person.Id,
                CreateDate = @event.Person.CreateDate,
                LastChangeDate = @event.Person.LastChangeDate,
                FirstName = @event.Person.FirstName,
                LastName = @event.Person.LastName,
                Gender = @event.Person.Gender,
                PersonalNumber = @event.Person.PersonalNumber,
                BirthDate = @event.Person.BirthDate,
                CityId = @event.Person.CityId,
                CityName = @event.Person.CityName,
                MobilePhoneNumber = @event.Person.PhoneNumbers?.FirstOrDefault(x => x.Type == PhoneNumberType.Mobile)?.Number,
                OfficePhoneNumber = @event.Person.PhoneNumbers?.FirstOrDefault(x => x.Type == PhoneNumberType.Office)?.Number,
                HomePhoneNumber = @event.Person.PhoneNumbers?.FirstOrDefault(x => x.Type == PhoneNumberType.Home)?.Number,
                PhotoUrl = @event.Person.Photo?.Url,
                PhotoWidth = @event.Person.Photo?.Width,
                PhotoHeight = @event.Person.Photo?.Height,
                PhotoId = @event.Person.PhotoId
            };

            db.Set<PersonReadModel>().Add(dto);
        }

        public void Handle(PersonChangedEvent @event, PersonBookDbContext db)
        {
            var personReadModel = db.Set<PersonReadModel>().FirstOrDefault(x => x.AggregateRootId == @event.AggregateRootId);

            if (personReadModel != null)
            {
                personReadModel.AggregateRootId = @event.Person.Id;
                personReadModel.CreateDate = @event.Person.CreateDate;
                personReadModel.LastChangeDate = @event.Person.LastChangeDate;
                personReadModel.FirstName = @event.Person.FirstName;
                personReadModel.LastName = @event.Person.LastName;
                personReadModel.Gender = @event.Person.Gender;
                personReadModel.PersonalNumber = @event.Person.PersonalNumber;
                personReadModel.BirthDate = @event.Person.BirthDate;
                personReadModel.CityId = @event.Person.CityId;
                personReadModel.CityName = @event.Person.CityName;
                personReadModel.MobilePhoneNumber = @event.Person.PhoneNumbers?.FirstOrDefault(x => x.Type == PhoneNumberType.Mobile)?.Number;
                personReadModel.OfficePhoneNumber = @event.Person.PhoneNumbers?.FirstOrDefault(x => x.Type == PhoneNumberType.Office)?.Number;
                personReadModel.HomePhoneNumber = @event.Person.PhoneNumbers?.FirstOrDefault(x => x.Type == PhoneNumberType.Home)?.Number;
                personReadModel.PhotoUrl = @event.Person.Photo?.Url;
                personReadModel.PhotoWidth = @event.Person.Photo?.Width;
                personReadModel.PhotoHeight = @event.Person.Photo?.Height;
                personReadModel.PhotoId = @event.Person.PhotoId;

                db.Set<PersonReadModel>().Update(personReadModel);
            }
        }

        public void Handle(PersonDeletedEvent @event, PersonBookDbContext db)
        {
            var personReadModel = db.Set<PersonReadModel>().FirstOrDefault(x => x.AggregateRootId == @event.AggregateRootId);

            if (personReadModel != null)
            {
                db.Set<PersonReadModel>().Remove(personReadModel);
            }
        }
    }
}
