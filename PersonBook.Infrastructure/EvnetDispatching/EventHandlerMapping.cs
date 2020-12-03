using PersonBook.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PersonBook.Infrastructure.EvnetDispatching
{
    public class EventHandlerMapping
    {
        public static IDictionary<Type, List<Type>> DomainEventHandlerMapping<THandler>(Assembly domainEventsAssembly, Assembly[] eventHandlersAssembly)
        {
            IDictionary<Type, List<Type>> result =
            new Dictionary<Type, List<Type>>();

            var domainEventTypes = domainEventsAssembly.GetTypes();

            var domainEvents = domainEventTypes
                .Where(at => typeof(DomainEvent).IsAssignableFrom(at)
                 && at.IsClass && !at.IsAbstract && !at.IsInterface);

            foreach (var domainEvent in domainEvents)
            {
                result[domainEvent] = new List<Type>();
                foreach (var assemblyType in eventHandlersAssembly.SelectMany(x => x.GetTypes()))
                {
                    var eventHandlers = assemblyType.GetInterfaces()
                        .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(THandler).GetGenericTypeDefinition());

                    if (eventHandlers != null)
                    {
                        foreach (var eventHandler in eventHandlers)
                        {
                            var genericarguments = eventHandler.GetGenericArguments().FirstOrDefault(x => domainEvent == x);
                            if (genericarguments != null)
                            {
                                result[domainEvent].Add(assemblyType);
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}
