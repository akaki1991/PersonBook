using Microsoft.EntityFrameworkCore;
using PersonBook.Domain.Shared;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PersonBook.Infrastructure.Shared
{
    public class EFBaseRepository<TContext, TAggregateRoot> : IRepository<TAggregateRoot>
       where TAggregateRoot : AggregateRoot
       where TContext : DbContext
    {
        TContext _context;

        public EFBaseRepository(TContext context)
        {
            _context = context;
        }

        public void Delete(TAggregateRoot aggregateRoot)
        {
            _context.Set<TAggregateRoot>().Remove(aggregateRoot);
        }

        public async Task<TAggregateRoot> OfIdAsync(int id)
        {
            return await _context.Set<TAggregateRoot>().FindAsync(id);
        }

        private void Insert(TAggregateRoot aggregateRoot)
        {
            _context.Set<TAggregateRoot>().Add(aggregateRoot);
        }

        public IQueryable<TAggregateRoot> Query(Expression<Func<TAggregateRoot, bool>> expression = null)
        {
            return expression == null ? _context.Set<TAggregateRoot>().AsQueryable() : _context.Set<TAggregateRoot>().Where(expression);
        }

        public void Save(TAggregateRoot aggregateRoot)
        {
            if (aggregateRoot.Id <= 0)
            {
                Insert(aggregateRoot);
            }
            else
            {
                Update(aggregateRoot);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        private void Update(TAggregateRoot aggregateRoot)
        {
            _context.Entry(aggregateRoot).State = EntityState.Modified;
        }
    }
}
