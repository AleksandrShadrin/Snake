using Snake.Application.Models;
using Snake.Core.ValueObjects;

namespace Snake.Presentation.Services
{
    public interface IGameSnakeRenderService
    {
        public void RenderSnakeBodyObject(SnakeBodyObject snakeBody);
        public void RenderWalls(IEnumerable<PosXY> walls);
        public void RenderRewardObjects(IEnumerable<RewardObject> rewards);
    }
}
