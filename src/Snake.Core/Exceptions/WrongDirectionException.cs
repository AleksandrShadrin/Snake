namespace Snake.Core.Exceptions
{
    public class WrongDirectionException : SnakeException
    {
        public WrongDirectionException() : base("Wrong direction was choosen.")
        { }
    }
}
