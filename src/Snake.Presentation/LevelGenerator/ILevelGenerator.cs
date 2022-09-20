using Snake.Core.ValueObjects;

namespace Snake.Presentation.LevelGenerator
{
    public interface ILevelGenerator
    {
        (PosXY, Level) GenerateLevel();
    }
}
