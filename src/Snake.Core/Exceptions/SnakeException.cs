namespace Snake.Core.Exceptions
{
    public abstract class SnakeException : Exception
    {
        protected SnakeException(string message) : base(message)
        { }
    }
}
