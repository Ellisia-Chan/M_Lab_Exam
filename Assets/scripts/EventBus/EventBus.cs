using System;
using System.Collections.Generic;

public static class EventBus {
    private static readonly Dictionary<Type, Delegate> eventTable = new Dictionary<Type, Delegate>();

    public static void Subscribe<T>(Action<T> callback) where T : class {
        if (eventTable.TryGetValue(typeof(T), out var existing)) {
            eventTable[typeof(T)] = Delegate.Combine(existing, callback);
        } else {
            eventTable[typeof(T)] = callback;
        }
    }

    public static void UnSubscribe<T>(Action<T> callback) where T: class {
        if (eventTable.TryGetValue(typeof(T), out var existing)) {
            var newDelegate = Delegate.Remove(existing, callback);

            if (newDelegate == null) {
                eventTable.Remove(typeof(T));
            } else {
                eventTable[typeof(T)] = newDelegate;
            }
        }
    }

    public static void Publish<T>(T evt) where T : class {
        if (eventTable.TryGetValue(typeof(T), out var callback)) {
            ((Action<T>)callback)?.Invoke(evt);
        }
    }
}
