using System;
using System.Collections.Generic;
using UnityEngine;

namespace HunterAllen.Events
{
    public static class EventManager
    {
        static Dictionary<object, Delegate> _events = new();

        #region Object Events
        // ============================================================================== Binding ==============================================================================
        /// <summary>
        /// Binds an Action to an object.
        /// </summary>
        /// <param name="e">The object to bind to.</param>
        /// <param name="a">The action.</param>
        public static void Bind(this object e, Action a)
        {
            if (_events.TryGetValue(e, out var current))
            {
                _events[e] = Delegate.Combine(a, current);
                return;
            }

            _events[e] = a;
        }
        /// <summary>
        /// Binds an Action with argument <typeparamref name="Argument"/> to an object.
        /// </summary>
        /// <param name="e">The object to bind to.</param>
        /// <param name="a">The action.</param>
        /// <typeparam name="Argument">Type of the argument.</typeparam>
        public static void Bind<Argument>(this object e, Action<Argument> a)
        {
            if (_events.TryGetValue(e, out var current))
            {
                _events[e] = Delegate.Combine(a, current);
                return;
            }

            _events[e] = a;
        }
        
        // ============================================================================= Unbinding ===========================================================================
        /// <summary>
        /// Unbinds an Action from an object.
        /// </summary>
        /// <param name="e">The object to unbind from.</param>
        /// <param name="a">The action.</param>
        public static void Unbind(this object e, Action a)
        {
            if (!_events.TryGetValue(e, out var action))
            {
                return;
            }

            var current = Delegate.Remove(action, a);

            if (current != null)
            {
                _events[e] = current;
                return;
            }

            _events.Remove(e);
        }
        /// <summary>
        /// Unbinds an Action with argument <typeparamref name="Argument"/> from an object.
        /// </summary>
        /// <param name="e">The object to unbind from.</param>
        /// <param name="a">The action.</param>
        /// <typeparam name="Argument">Type of the argument.</typeparam>
        public static void Unbind<Argument>(this object e, Action<Argument> a)
        {
            if (!_events.TryGetValue(e, out var action))
            {
                return;
            }

            var current = Delegate.Remove(action, a);

            if (current != null)
            {
                _events[e] = current;
                return;
            }

            _events.Remove(e);
        }
        
        // ============================================================================== Raising ============================================================================
        /// <summary>
        /// Raises an Event via object.
        /// </summary>
        /// <param name="e">The object binded to the event.</param>
        public static void Raise(this object e)
        {
            if (_events.TryGetValue(e, out var a))
            {
                ((Action)a)?.Invoke();
            }
        }
        /// <summary>
        /// Raises an Event with argument <typeparamref name="Argument"/> via object.
        /// </summary>
        /// <param name="e">The object binded to the event.</param>
        /// <typeparam name="Argument">Type of the argument.</typeparam>
        public static void Raise<Argument>(this object e, Argument arg)
        {
            if (_events.TryGetValue(e, out var a))
            {
                ((Action<Argument>)a)?.Invoke(arg);
            }
        }
        #endregion

        #region Type Events
        // ============================================================================== Binding ==============================================================================
        /// <summary>
        /// Binds an Action to an Event.
        /// </summary>
        /// <param name="a">The action.</param>
        /// <typeparam name="EventType">Type of the event.</typeparam>
        public static void TBind<EventType>(Action a) where EventType : IEvent
        {
            var type = typeof(EventType);

            if (_events.TryGetValue(type, out var current))
            {
                _events[type] = Delegate.Combine(a, current);
                return;
            }

            _events[type] = a;
        }
        /// <summary>
        /// Binds an Action with argument <typeparamref name="Argument"/> to an Event.
        /// </summary>
        /// <param name="a">The action.</param>
        /// <typeparam name="EventType">Type of the event.</typeparam>
        /// <typeparam name="Argument">Type of the argument.</typeparam>
        public static void TBind<EventType, Argument>(Action<Argument> a) where EventType : IEvent
        {
            var type = typeof(EventType);

            if (_events.TryGetValue(type, out var current))
            {
                _events[type] = Delegate.Combine(a, current);
                return;
            }

            _events[type] = a;
        }
        // ============================================================================ Extensions ==============================================================================
        /// <summary>
        /// Binds an Action to an Event.
        /// </summary>
        /// <param name="e">The event.</param>
        /// <param name="a">The action.</param>
        /// <typeparam name="EventType">Type of the event.</typeparam>
        public static void TBind<EventType>(this EventType e, Action a) where EventType : IEvent
        {
            var type = typeof(EventType);

            if (_events.TryGetValue(type, out var current))
            {
                _events[type] = Delegate.Combine(a, current);
                return;
            }

            _events[type] = a;
        }
        /// <summary>
        /// Binds an Action with argument <typeparamref name="Argument"/> to an Event.
        /// </summary>
        /// <param name="e">The event.</param>
        /// <param name="a">The action.</param>
        /// <typeparam name="EventType">Type of the event.</typeparam>
        /// <typeparam name="Argument">Type of the argument.</typeparam>
        public static void TBind<EventType, Argument>(this EventType e, Action<Argument> a) where EventType : IEvent
        {
            var type = e.GetType();

            if (_events.TryGetValue(type, out var current))
            {
                _events[type] = Delegate.Combine(a, current);
                return;
            }

            _events[type] = a;
        }

        // ============================================================================= Unbinding ===========================================================================
        /// <summary>
        /// Unbinds an Action from an Event.
        /// </summary>
        /// <param name="a">The action.</param>
        /// <typeparam name="EventType">Type of the event.</typeparam>
        public static void TUnbind<EventType>(Action a) where EventType : IEvent
        {
            var type = typeof(EventType);

            if (!_events.TryGetValue(type, out var action))
            {
                return;
            }

            var current = Delegate.Remove(action, a);

            if (current != null)
            {
                _events[type] = current;
                return;
            }

            _events.Remove(type);
        }
        /// <summary>
        /// Unbinds an Action with argument <typeparamref name="Argument"/> from an Event.
        /// </summary>
        /// <param name="e">The event.</param>
        /// <param name="a">The action.</param>
        /// <typeparam name="EventType">Type of the event.</typeparam>
        /// <typeparam name="Argument">Type of the argument.</typeparam>
        public static void TUnbind<EventType, Argument>(Action<Argument> a) where EventType : IEvent
        {
            var type = typeof(EventType);

            if (!_events.TryGetValue(type, out var action))
            {
                return;
            }

            var current = Delegate.Remove(action, a);

            if (current != null)
            {
                _events[type] = current;
                return;
            }

            _events.Remove(type);
        }
        // ============================================================================ Extensions ==============================================================================
        /// <summary>
        /// Unbinds an Action from an Event.
        /// </summary>
        /// <param name="e">The event.</param>
        /// <param name="a">The action.</param>
        /// <typeparam name="EventType">Type of the event.</typeparam>
        public static void TUnbind<EventType>(this EventType e, Action a) where EventType : IEvent
        {
            var type = typeof(EventType);

            if (!_events.TryGetValue(type, out var action))
            {
                return;
            }

            var current = Delegate.Remove(action, a);

            if (current != null)
            {
                _events[type] = current;
                return;
            }

            _events.Remove(type);
        }
        /// <summary>
        /// Unbinds an Action with argument <typeparamref name="Argument"/> from an Event.
        /// </summary>
        /// <param name="e">The event.</param>
        /// <param name="a">The action.</param>
        /// <typeparam name="EventType">Type of the event.</typeparam>
        /// <typeparam name="Argument">Type of the argument.</typeparam>
        public static void TUnbind<EventType, Argument>(this EventType e, Action<Argument> a) where EventType : IEvent
        {
            var type = e.GetType();

            if (!_events.TryGetValue(type, out var action))
            {
                return;
            }

            var current = Delegate.Remove(action, a);

            if (current != null)
            {
                _events[type] = current;
                return;
            }

            _events.Remove(type);
        }

        // ============================================================================== Raising ============================================================================
        /// <summary>
        /// Raises an Event.
        /// </summary>
        /// <typeparam name="EventType">Type of the event.</typeparam>
        public static void TRaise<EventType>() where EventType : IEvent
        {
            var type = typeof(EventType);
            if (_events.TryGetValue(type, out var a))
            {
                ((Action)a)?.Invoke();
            }
        }
        /// <summary>
        /// Raises an Event with argument <typeparamref name="Argument"/>.
        /// </summary>
        /// <typeparam name="EventType">Type of the event.</typeparam>
        /// <typeparam name="Argument">Type of the argument.</typeparam>
        public static void TRaise<EventType, Argument>(Argument arg) where EventType : IEvent
        {
            var type = typeof(EventType);
            if (_events.TryGetValue(type, out var a))
            {
                ((Action<Argument>)a)?.Invoke(arg);
            }
        }
        // ============================================================================ Extensions ==============================================================================
        /// <summary>
        /// Raises an Event.
        /// </summary>
        /// <param name="e">The event.</param>
        /// <typeparam name="EventType">Type of the event.</typeparam>
        public static void TRaise<EventType>(this EventType e) where EventType : IEvent
        {
            var type = typeof(EventType);
            if (_events.TryGetValue(type, out var a))
            {
                ((Action)a)?.Invoke();
            }
        }
        /// <summary>
        /// Raises an Event with argument <typeparamref name="Argument"/>.
        /// </summary>
        /// <param name="e">The object binded to the event.</param>
        /// <typeparam name="EventType">Type of the event.</typeparam>
        /// <typeparam name="Argument">Type of the argument.</typeparam>
        public static void TRaise<EventType, Argument>(this EventType e, Argument arg) where EventType : IEvent
        {
            var type = typeof(EventType);
            if (_events.TryGetValue(type, out var a))
            {
                ((Action<Argument>)a)?.Invoke(arg);
            }
        }
        #endregion

        #region String Events
        /*
        /// <summary>
        /// Binds an Action to an Event Channel.
        /// </summary>
        /// <param name="s">The Event Channel.</param>
        /// <param name="a">The action.</param>
        public static void SBind(string s, Action a)
        {
            if (_events.TryGetValue(s, out var current))
            {
                _events[s] = Delegate.Combine(a, current);
                return;
            }

            _events[s] = a;
        }
        /// <summary>
        /// Binds an Action to an Event Channel.
        /// </summary>
        /// <param name="s">The Event Channel.</param>
        /// <param name="a">The action.</param>
        public static void SBind<Argument>(string s, Action<Argument> a)
        {
            if (_events.TryGetValue(s, out var current))
            {
                _events[s] = Delegate.Combine(a, current);
                return;
            }

            _events[s] = a;
        }
        /// <summary>
        /// Unbinds an Action from an Event Channel.
        /// </summary>
        /// <param name="s">The Event Channel.</param>
        /// <param name="a">The action.</param>
        public static void SUnbind(string s, Action a)
        {
            if (!_events.TryGetValue(s, out var action))
            {
                return;
            }

            var current = Delegate.Remove(action, a);

            if (current != null)
            {
                _events[s] = current;
                return;
            }

            _events.Remove(s);
        }
        /// <summary>
        /// Unbinds an Action from an Event Channel.
        /// </summary>
        /// <param name="s">The Event Channel.</param>
        /// <param name="a">The action.</param>
        public static void SUnbind<Argument>(string s, Action<Argument> a)
        {
            if (!_events.TryGetValue(s, out var action))
            {
                return;
            }

            var current = Delegate.Remove(action, a);

            if (current != null)
            {
                _events[s] = current;
                return;
            }

            _events.Remove(s);
        }
        /// <summary>
        /// Raises an Event Channel.
        /// </summary>
        /// <param name="s">The Event Channel.</param>
        public static void SRaise(string s)
        {
            if (_events.TryGetValue(s, out var a))
            {
                ((Action)a)?.Invoke();
            }
        }
        /// <summary>
        /// Raises an Event Channel.
        /// </summary>
        /// <param name="s">The Event Channel.</param>
        public static void SRaise<Argument>(string s, Argument t)
        {
            if (_events.TryGetValue(s, out var a))
            {
                ((Action<Argument>)a)?.Invoke(t);
            }
        }
        */
        #endregion
    }
}