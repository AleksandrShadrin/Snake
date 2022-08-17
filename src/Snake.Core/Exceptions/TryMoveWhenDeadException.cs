namespace Snake.Core.Exceptions
{
    public class TryMoveWhenDeadException : SnakeException
    {
        public TryMoveWhenDeadException() : base("Can't move because snake is dead.")
        { }
    }
}
