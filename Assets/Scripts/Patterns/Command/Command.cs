namespace Patterns
{
    /// <summary>
    ///     Pretty good description with an UML diagram on the link below.
    ///     Refs: https://java-design-patterns.com/patterns/command/
    /// </summary>
    public abstract class Command
    {
        public abstract void Execute();
        public abstract void Undo();
    }
}