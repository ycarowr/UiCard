using System;
using System.Collections.Generic;

namespace Patterns
{
    //--------------------------------------------------------------------------------------------------------------

    #region Interfaces 

    /// <summary>
    ///     All classes that are listened by IListener.
    /// </summary>
    public interface ISubject
    {
    }

    /// <summary>
    ///     All classes that are listening ISubject.
    /// </summary>
    public interface IListener
    {
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    //TODO: Consider a refactor to adapt the implementation using an Attribute instead only 
    //TODO: Interfaces, because mid level interfaces cause a duplicated register as [Key, Listener].
    //TODO: for instance: ISubject -> IGameEvent -> IMyInterface impacts in two keys inside the register.
    /// <summary>
    ///     This class contains a register that contains Subjects and their respective Listeners.
    ///     Every time a Subject value is updated and notified, this pattern broadcast the modification to the Listeners.
    ///     Refs:
    ///     1. https://forum.unity.com/threads/observer-pattern-hell.219749/
    ///     2. https://www.youtube.com/watch?v=Yy7Dt2usGy0
    ///     3. https://www.habrador.com/tutorials/programming-patterns/3-observer-pattern/
    ///     4. https://forum.unity.com/threads/observer-design-pattern-with-game-objects.388713/
    ///     5. https://jacekrojek.github.io/JacekRojek/2016/c-observer-design-pattern/
    ///     6. https://docs.microsoft.com/en-us/dotnet/standard/events/how-to-implement-a-provider
    /// </summary>
    public class Observer<T> : SingletonMB<Observer<T>>
    {
        //--------------------------------------------------------------------------------------------------------------

        #region Fields

        /// <summary>
        ///     This is the Subject-Listener register. It supports both, Monobehaviors and
        ///     Pure C# classes that implement ISubject and IListener interfaces.
        /// </summary>
        readonly Dictionary<Type, List<IListener>>
            register = new Dictionary<Type, List<IListener>>();

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Operations 

        /// <summary>
        ///     Register a listener as well as its implemented interfaces subjects.
        /// </summary>
        /// <param name="listener"></param>
        public void AddListener(IListener listener)
        {
            if (listener == null)
                throw new ArgumentNullException("Can't register Null as a Listener");

            //find the type of object
            var type = listener.GetType();

            //get all implemented interfaces by the type class
            var interfaces = type.GetInterfaces();

            //iterate on all implemented interfaces
            for (var i = 0; i < interfaces.Length; i++)
            {
                var subject = interfaces[i];

                //TODO: ISubject and mid level interfaces are also added to the register
                var isAssignableFrom = typeof(ISubject).IsAssignableFrom(subject);
                //if the interface is a Subject, add the pair [Subject, Listener]
                if (isAssignableFrom)
                    CreateAndAdd(subject, listener);
            }
        }


        /// <summary>
        ///     Removes a listener from the register. Unlike the AddListener method this method doesn't affect the Key Register.
        /// </summary>
        /// <param name="listener"></param>
        public void RemoveListener(IListener listener)
        {
            foreach (var pair in register)
                pair.Value.Remove(listener);
        }


        /// <summary>
        ///     Removes a subject and all its listeners from the register.
        /// </summary>
        /// <param name="subject"></param>
        public void RemoveSubject(Type subject)
        {
            if (register.ContainsKey(subject))
                register.Remove(subject);
        }


        /// <summary>
        ///     Broadcasts the subject interface T1 for all listeners
        ///     Usage: Notify<T1>(i => i.subject(subject params)).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="subject"></param>
        public void Notify<T1>(Action<T1> subject) where T1 : class
        {
            var subjectType = typeof(T1);

            //if the type is a subject move forward
            var isSubject = register.ContainsKey(subjectType);
            if (!isSubject)
                return;

            //if there are register for this subject move forward
            var listeners = register[subjectType];
            if (listeners.Count == 0) return;

            //broadcast for all gotten register
            for (var i = 0; i < listeners.Count; i++)
            {
                var obj = listeners[i];
                if (obj != null)
                    subject(obj as T1);
            }
        }


        /// <summary>
        ///     Create the listeners register and add the it according to its own subject Interface.
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="listener"></param>
        void CreateAndAdd(Type subject, IListener listener)
        {
            if (register.ContainsKey(subject))
                register[subject].Add(listener);
            else
                register.Add(subject, new List<IListener> {listener});
        }

        #endregion

        //--------------------------------------------------------------------------------------------------------------
    }
}