namespace Snake.Core.Exceptions
{
    public class RewardObjectCantBeInWallPosException : SnakeException
    {
        public RewardObjectCantBeInWallPosException() : base("Invalid position for reward object.")
        {
        }
    }
}
