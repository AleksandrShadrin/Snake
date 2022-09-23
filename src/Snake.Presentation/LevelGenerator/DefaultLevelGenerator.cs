using Snake.Core.ValueObjects;

namespace Snake.Presentation.LevelGenerator
{
    internal class DefaultLevelGenerator : ILevelGenerator
    {
        public (PosXY, Level) GenerateLevel()
        => (new PosXY(0, 0), new Level(new PosXY(Console.WindowWidth - 1, Console.WindowHeight - 1), Enumerable.Empty<PosXY>()));
    }
}
