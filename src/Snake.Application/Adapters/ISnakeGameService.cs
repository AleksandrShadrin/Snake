using Snake.Application.Models;
using Snake.Core.Constants;
using Snake.Core.ValueObjects;

namespace Snake.Application.Adapters
{
    public interface ISnakeGameService
    {
        /// <summary>
        /// Возвращает текущее направление змейки
        /// </summary>
        /// <returns>Direction - направление змейки</returns>
        Direction CurrentDirection();

        /// <summary>
        /// Создаёт обычную игру, с заданными начальным положением и уровнем
        /// </summary>
        /// <param name="pos">начальное положение змейки</param>
        /// <param name="level">уровень (стены, награды)</param>
        void CreateGame(PosXY pos, Level level);

        /// <summary>Проверка окончания игры</summary>
        /// <returns>true, если игра окончена</returns>
        bool GameIsOver();

        /// <summary>Возвращает текущее количество очков</summary>
        /// <returns>положительное число</returns>
        uint GetScore();

        /// <summary>
        /// Добавляет награду
        /// </summary>
        /// <param name="reward">RewardObject, который будет добавлен в игру</param>
        void AddRewardObject(RewardObject reward);

        /// <summary>
        /// Удаляет награду
        /// </summary>
        /// <param name="reward">RewardObject, который будет удален из игры</param>
        void RemoveRewardObject(RewardObject reward);

        /// <summary>
        /// Перемещает змейку на один шаг
        /// </summary>
        void MoveSnake();

        /// <summary>
        /// Изменяет направление змейки
        /// </summary>
        /// <param name="direction">Direction, направление на которое пытаются поменять текущее значение</param>
        void ChangeMoveDirection(Direction direction);

        /// <summary>
        /// Загрузка игры
        /// </summary>
        /// <param name="data">SnakeGameData, объект хранящий состояние игры</param>
        void LoadGame(SnakeGameData data);

        /// <summary>
        /// Вовзращает текущий Level
        /// </summary>
        /// <returns>Возвращает объект Level</returns>
        Level GetLevel();

        /// <summary>
        /// Вовзращает текущий Level
        /// </summary>
        /// <returns>Возвращает объект Level</returns>
        IEnumerable<PosXY> GetWalls();

        /// <summary>
        /// Вовзращает награды
        /// </summary>
        /// <returns>Возвращает коллекцию наград</returns>
        IReadOnlyList<RewardObject> GetRewards();

        /// <summary>
        /// Вовзращает тело змейку
        /// </summary>
        /// <returns>Возвращает объект SnakeBodyObject</returns>
        SnakeBodyObject GetSnakeBody();
    }
}
