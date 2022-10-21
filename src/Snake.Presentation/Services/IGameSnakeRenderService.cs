using Snake.Application.Models;
using Snake.Core.Constants;
using Snake.Core.ValueObjects;

namespace Snake.Presentation.Services
{
    public interface IGameSnakeRenderService
    {
        /// <summary>
        /// Рендерит змейку
        /// </summary>
        /// <param name="snakeBody">тело змейки</param>
        /// <param name="direction">направление змейки</param>
        public void RenderSnakeBodyObject(SnakeBodyObject snakeBody, Direction direction);

        /// <summary>
        /// Рендерит стенки
        /// </summary>
        /// <param name="walls">стены</param>
        public void RenderWalls(IEnumerable<PosXY> walls);

        /// <summary>
        /// Рендерит награды
        /// </summary>
        /// <param name="rewards"> награды</param>
        public void RenderRewardObjects(IEnumerable<RewardObject> rewards);
    }
}
