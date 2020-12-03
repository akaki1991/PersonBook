using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PersonBook.Domain.Shared
{
    public interface IRepository<TAggregateRoot> where TAggregateRoot : class
    {
        Task<TAggregateRoot> OfIdAsync(int id);

        void Delete(TAggregateRoot aggregateRoot);

        void Save(TAggregateRoot aggregateRoot);

        IQueryable<TAggregateRoot> Query(Expression<Func<TAggregateRoot, bool>> expression = null);
    }
}
