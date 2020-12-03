using PersonBook.Domain.CityAggregate.Repositories;
using PersonBook.Domain.PersonAggregate.Repositories;
using PersonBook.Domain.PersonRelationAggregate.Repositories;
using PersonBook.Domain.PhotoAggregate.Repositories;
using PersonBook.Infrastructure.Db;
using System;

namespace PersonBook.Application.Infrastructure
{
    public abstract class ApplicationBase
    {
        protected PersonBookDbContext _db;
        protected UnitOfWork _unitOfWork;
        protected IServiceProvider _serviceProvider;
        protected IPersonRepository _personRepository;
        protected IPersonRelationshipRepository _personRelationshipRepository;
        protected ICityRepository _cityRepository;
        protected IPhotoRepository _photoRepository;

        protected ApplicationContext ApplicationContext { get; private set; }

        public void Resolve(PersonBookDbContext db,
                            UnitOfWork unitOfWork,
                            IServiceProvider serviceProvider,
                            ApplicationContext applicationContext)
        {
            _db = db;
            _unitOfWork = unitOfWork;
            _serviceProvider = serviceProvider;
            ApplicationContext = applicationContext;
            _personRepository = GetService<IPersonRepository>();
            _personRelationshipRepository = GetService<IPersonRelationshipRepository>();
            _cityRepository = GetService<ICityRepository>();
            _photoRepository = GetService<IPhotoRepository>();
        }

        public T GetService<T>() => (T)_serviceProvider.GetService(typeof(T));
    }
}
