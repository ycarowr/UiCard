namespace Patterns
{
    public abstract class DataBuilder<T>
    {
        public abstract T Build();

        public static implicit operator T(DataBuilder<T> builder) => builder.Build();
    }
}