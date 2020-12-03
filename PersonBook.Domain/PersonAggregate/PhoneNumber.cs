using PersonBook.Domain.Shared;

namespace PersonBook.Domain.PersonAggregate
{
    public class PhoneNumber : Entity
    {
        protected PhoneNumber()
        {
        }

        public PhoneNumber(string number, PhoneNumberType type)
        {
            Number = number;
            Type = type;
        }

        public string Number { get; private set; }

        public PhoneNumberType Type { get; private set; }

        public int? PersonId { get; set; }

        public virtual Person Person { get; private set; }
    }
}
