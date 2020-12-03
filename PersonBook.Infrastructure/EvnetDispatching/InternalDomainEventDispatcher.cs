using PersonBook.Domain.Shared;
using PersonBook.Infrastructure.Db;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace PersonBook.Infrastructure.EvnetDispatching
{
    public class InternalDomainEventDispatcher : IInternalEventDispatcher<DomainEvent>
    {
        private readonly IDictionary<Type, List<Type>> eventhandlerMaps =
            new Dictionary<Type, List<Type>>();
        private readonly IServiceProvider serviceProvider;

        public InternalDomainEventDispatcher(IServiceProvider serviceProvider, Assembly domainEventsAssembly,
            params Assembly[] eventHandlersAssemblies)
        {
            eventhandlerMaps = EventHandlerMapping.DomainEventHandlerMapping<IHandleEvent<DomainEvent>>(domainEventsAssembly, eventHandlersAssemblies);
            this.serviceProvider = serviceProvider;
        }

        private void Dispatch(dynamic evnt, PersonBookDbContext dbContext)
        {
            var type = evnt.GetType();
            if (!eventhandlerMaps.ContainsKey(type))
                return;
            var @eventHandlers = eventhandlerMaps[type];
            foreach (var handlr in @eventHandlers)
            {
                var domainEventHandler = serviceProvider.GetService(handlr);
                if (domainEventHandler == null)
                {
                    domainEventHandler = Activator.CreateInstance(handlr);
                }
                var handlerInstance = domainEventHandler as dynamic;
                handlerInstance.Handle(evnt, dbContext);
            }
        }

        public void Dispatch(IReadOnlyList<DomainEvent> events, PersonBookDbContext dbContext)
        {
            foreach (var item in events)
            {
                Dispatch(item, dbContext);
            }
        }
    }
}
