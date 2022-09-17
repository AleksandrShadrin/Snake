using Snake.Application.Models;
using Snake.Core.ValueObjects;
using System.Xml.Linq;

namespace Snake.Presentation.Services
{
    public class ConsoleGameSnakeRenderService : IGameSnakeRenderService
    { 
        public void RenderRewardObjects(IEnumerable<RewardObject> rewards)
        {
            rewards.ToList().ForEach(i =>
            {
                Console.SetCursorPosition(i.Position.X, i.Position.Y);
                Console.WriteLine('*');
            });
        }

        public void RenderSnakeBodyObject(SnakeBodyObject snake)
        {
            var allBody = new List<PosXY>();
            allBody.AddRange(snake.Body);
            allBody.Add(snake.Head);
            allBody.Add(snake.Tail);
            allBody.ForEach(i =>
            {
                if (i == null)
                    return;
                Console.SetCursorPosition(i.X, i.Y);
                Console.WriteLine('\u2500');
            });
        }

        public void RenderWalls(IEnumerable<PosXY> walls)
        {
            walls.ToList().ForEach(i =>
            {
                Console.SetCursorPosition(i.X, i.Y);
                Console.WriteLine('\u2593');
            });
        }
    }
}
