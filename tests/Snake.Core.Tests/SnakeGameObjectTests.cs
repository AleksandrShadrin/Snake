using Shouldly;
using Snake.Core.Exceptions;
using Snake.Core.Factories;
using Snake.Core.ValueObjects;

namespace Snake.Core.Tests
{
    public class SnakeGameObjectTests
    {
        [Fact]
        public void SnakeLifeState_Change_Correctly()
        {
            // Arrange
            var startPos = new PosXY(0, 0);
            var snake = snakeGameObjectFactory.CreateSnakeGameObject(startPos);

            // Act
            snake.KillSnake();

            // Assert
            snake.SnakeIsDead().ShouldBeTrue();
            snake.SnakeIsAlive().ShouldBeFalse();
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 2)]
        public void Snake_Should_Collide_With_Object_At_Pos(int x, int y)
        {
            // Arrange
            var startPos = new PosXY(x, y);
            var snake = snakeGameObjectFactory.CreateSnakeGameObject(startPos);

            // Act
            var result = snake.CheckCollisionAtPosition(startPos);

            // Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public void When_Snake_Is_Dead_Move_Will_Throw_TryMoveWhenDeadException()
        {
            // Arrange
            var startPos = new PosXY(0, 0);
            var snake = snakeGameObjectFactory.CreateSnakeGameObject(startPos);

            // Act
            snake.KillSnake();
            var exception = Record.Exception(() => snake.Move(startPos with { X = 1 }));

            // Assert
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<TryMoveWhenDeadException>();
        }
        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(7, 6)]
        public void Test_Body_Count_When_Increasing_Snake_With_Moving(int increaseCount, int bodySize)
        {
            // Arrange
            var startPos = new PosXY(0, 0);
            var snake = snakeGameObjectFactory.CreateSnakeGameObject(startPos);

            // Act
            for (int i = 0; i < increaseCount; i++)
            {
                snake.IncreaseSnake();
                snake.Move(startPos with { X = i + 1});
            }

            // Assert
            snake.GetBody().Count().ShouldBe(bodySize);
        }
        [Fact]
        public void When_Snake_Was_Not_Increased_Tail_Is_Null()
        {
            // Arrange
            var startPos = new PosXY(0, 0);
            var snake = snakeGameObjectFactory.CreateSnakeGameObject(startPos);

            // Act

            // Assert
            snake.GetTail().ShouldBeNull();
        }
        #region ARRANGE
        private readonly ISnakeGameObjectFactory snakeGameObjectFactory
            = new SnakeGameObjectFactory();
        #endregion
    }
}
