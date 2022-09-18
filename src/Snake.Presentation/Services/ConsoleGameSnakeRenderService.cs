using Snake.Application.Models;
using Snake.Core.Constants;
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

        public void RenderSnakeBodyObject(SnakeBodyObject snake, Direction direction)
        {
            var allBody = new List<PosXY>();

            if (snake.Tail is { })
            {
                allBody.Add(snake.Tail);
            }
            allBody.AddRange(snake.Body);
            allBody.Add(snake.Head);

            if (allBody.Count > 1)
            {
                var dir = CheckDirection(allBody[0], allBody.Where(p => p != snake.Tail).First());
                Console.SetCursorPosition(snake.Tail.X, snake.Tail.Y);
                if (dir is Direction.RIGHT or Direction.LEFT)
                {
                    Console.WriteLine(GetHeadSymbol((Direction)(-(int)dir)));
                }
                else
                {
                    Console.WriteLine(GetHeadSymbol(dir));
                }
            }

            Console.SetCursorPosition(snake.Head.X, snake.Head.Y);
            Console.WriteLine(GetHeadSymbol(direction));

            PrintBody(allBody);
        }

        public void RenderWalls(IEnumerable<PosXY> walls)
        {
            walls.ToList().ForEach(i =>
            {
                Console.SetCursorPosition(i.X, i.Y);
                Console.WriteLine('\u2593');
            });
        }
        private void PrintBody(List<PosXY> allBody)
        {
            if (allBody.Count >= 3)
            {
                for (int i = 1; i < allBody.Count - 1; i++)
                {
                    var firstDirection = CheckDirection(allBody[i], allBody[i - 1]);
                    var secondDirection = CheckDirection(allBody[i + 1], allBody[i]);

                    Console.SetCursorPosition(allBody[i].X, allBody[i].Y);
                    if (firstDirection == secondDirection)
                    {
                        if (firstDirection is Direction.RIGHT or Direction.LEFT)
                        {
                            Console.WriteLine('\u2550');
                        }
                        else
                        {
                            Console.WriteLine('\u2551');
                        }
                    }
                    else
                    {
                        if (firstDirection is Direction.LEFT)
                        {
                            if (secondDirection is Direction.BOTTOM)
                            {
                                Console.WriteLine('\u2557');
                            }
                            else
                            {
                                Console.WriteLine('\u255D');
                            }
                        }
                        else if (firstDirection is Direction.RIGHT)
                        {
                            if (secondDirection is Direction.TOP)
                            {
                                Console.WriteLine('\u255A');
                            }
                            else
                            {
                                Console.WriteLine('\u2554');
                            }
                        }
                        else if (firstDirection is Direction.TOP)
                        {
                            if (secondDirection is Direction.LEFT)
                            {
                                Console.WriteLine('\u2554');
                            }
                            else
                            {
                                Console.WriteLine('\u2557');
                            }
                        }
                        else
                        {
                            if (secondDirection is Direction.LEFT)
                            {
                                Console.WriteLine('\u255A');
                            }
                            else
                            {
                                Console.WriteLine('\u255D');
                            }
                        }
                    }
                }
            }
        }
        private Direction CheckDirection(PosXY first, PosXY second)
        {
            var resultX = first.X - second.X;
            var resultY = first.Y - second.Y;

            if (resultX < 0)
                return Direction.RIGHT;
            else if (resultX > 0)
                return Direction.LEFT;

            if (resultY < 0)
                return Direction.TOP;
            else
                return Direction.BOTTOM;
        }

        private char GetHeadSymbol(Direction direction)
        {
            return direction switch
            {
                Direction.TOP => 'ᐃ',
                Direction.BOTTOM => 'ᐁ',
                Direction.LEFT => 'ᐊ',
                Direction.RIGHT => 'ᐅ'
            };
        }
    }
}
