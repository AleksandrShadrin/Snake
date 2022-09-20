namespace Snake.Core.Exceptions
{
    public class TryChangeSnakeDirectionMoreThenOncePerMoveException : SnakeException
    {
        public TryChangeSnakeDirectionMoreThenOncePerMoveException() : base("Can't change direction more than one time per move.")
        {
        }
    }
}
