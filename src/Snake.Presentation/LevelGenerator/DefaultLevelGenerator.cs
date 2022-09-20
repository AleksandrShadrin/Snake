using Snake.Core.ValueObjects;

namespace Snake.Presentation.LevelGenerator
{
    internal class DefaultLevelGenerator : ILevelGenerator
    {
        public (PosXY, Level) GenerateLevel()
        => (new PosXY(0, 0), new Level(new PosXY(30, 30), Enumerable.Empty<PosXY>()));
    }
}
