namespace Patterns
{
    /// <summary>
    ///     Pure C# Singleton Pattern. Refs below:
    ///     1. https://stackoverflow.com/questions/2319075/generic-singletont
    ///     2. https://codereview.stackexchange.com/questions/10554/a-generic-singleton
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> where T : class, new()
    {
        //a protected constructor
        protected Singleton()
        {
        }

        //public getter
        public static T Instance { get; private set; } = CreateInstance();

        static T CreateInstance() => Instance ?? (Instance = new T());

        //Setter used to inject an instance 
        public void InjectInstance(T instance)
        {
            if (instance != null)
                Instance = instance;
        }
    }
}