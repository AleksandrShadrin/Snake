using Snake.Core.ValueObjects;

namespace Snake.Presentation.LevelGenerator
{
    public interface ILevelGenerator
    {
        /// <summary>
        /// Генерирует уровень, с позицией змейки
        /// </summary>
        /// <returns>Возвращает tuple с item1 - позиция змейки, item2 - уровень</returns>
        (PosXY, Level) GenerateLevel();
    }
}
