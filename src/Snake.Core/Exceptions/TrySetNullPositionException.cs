namespace Snake.Core.Exceptions
{
    public class TrySetNullPositionException : SnakeException
    {
        public TrySetNullPositionException() : base("Trying to set null PosXY object.")
        {
        }
    }
}
