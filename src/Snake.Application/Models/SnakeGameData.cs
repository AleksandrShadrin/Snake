using Snake.Core.Constants;
using Snake.Core.ValueObjects;

namespace Snake.Application.Models
{
    public record SnakeGameData(SnakeBodyObject Snake, List<RewardObject> RewardObjects, Level Level, Direction Direction);
}
