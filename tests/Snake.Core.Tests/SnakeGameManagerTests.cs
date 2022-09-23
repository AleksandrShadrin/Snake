using Shouldly;
using Snake.Core.Domain;
using Snake.Core.Exceptions;
using Snake.Core.Factories;
using Snake.Core.ValueObjects;

namespace Snake.Core.Tests
{
    public class SnakeGameManagerTests
    {
        [Fact]
        public void SnakeGameManager_Should_Throw_WrongDirectionException_When_Choose_Reverse_Direction()
        {
            // Arrange
            var gameManager = GetGameManagerWithDeafaultParams();

            // Act
            var exception = Record.Exception(() => gameManager.ChangeDirection(Constants.Direction.LEFT));

            // Assert
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<WrongDirectionException>();
        }

        [Fact]
        public void Reward_Object_That_Added_At_Position_That_Contain_Other_Reward_Object_Will_Update_It()
        {
            // Arrange
            var firstRewardObject = new RewardObject(new PosXY(1, 1), 4);
            var secondRewardObject = new RewardObject(new PosXY(1, 1), 8);
            var gameManager = GetGameManagerWithDeafaultParams();

            // Act
            gameManager.AddRewardObject(firstRewardObject);
            gameManager.AddRewardObject(secondRewardObject);

            // Assert
            gameManager.GetRewardObjects().Count.ShouldBe(1);
            gameManager.GetRewardObjects().First().Reward.ShouldBe((uint)8);
        }
        [Fact]
        public void When_SnakeGameObject_Is_Dead_GameManager_GameOver_Should_Be_True()
        {
            // Arrange
            var snake = GetSnakeInDefaultPosition();
            var gameManager = snakeGameManagerFactory
                .CreateSnakeGameManager(snake,
                    new Level(new PosXY(10, 10),
                        Enumerable.Empty<PosXY>()));

            // Act
            snake.KillSnake();

            // Assert
            gameManager.GameIsOver().ShouldBeTrue();
        }

        [Fact]
        public void Game_Should_Be_Over_If_GameOver_Condition_For_Score_Will_Be_Added()
        {
            // Arrange
            var gameManager = GetGameManagerWithDeafaultParams();

            // Act
            gameManager.AddGameOverConditions((gm) => gm.Score >= 5);
            gameManager.AddScore(5);

            // Assert
            gameManager.GameIsOver().ShouldBeTrue();
        }

        [Fact]
        public void If_Field_Is_End_Snake_Should_Appear_At_Reverse_End()
        {
            // Arrange
            var snake = GetSnakeInDefaultPosition();
            var gameManager = snakeGameManagerFactory
                .CreateSnakeGameManager(snake,
                    new Level(new PosXY(5, 10),
                        Enumerable.Empty<PosXY>()));

            // Act
            for (int i = 0; i < 5; i++)
            {
                gameManager.MoveSnake();
            }

            // Assert
            snake.GetHead().ShouldBe(new PosXY(0, 0));
        }

        [Fact]
        public void When_Add_Reward_Object_In_PosXY_Where_Wall_Already_Exist_Should_Throw_RewardObjectCantBeInWallPosException()
        {
            // Arrange
            var snake = GetSnakeInDefaultPosition();
            var gameManager = snakeGameManagerFactory
                .CreateSnakeGameManager(snake,
                    new Level(new PosXY(5, 10),
                        new List<PosXY> { new PosXY(1, 1) }));
            var rewardObject = new RewardObject
                    (
                        Position: new PosXY(1, 1),
                        Reward: 4
                    );

            // Act
            var exception = Record.Exception(() => gameManager
                .AddRewardObject(rewardObject));

            // Assert
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<RewardObjectCantBeInWallPosException>();
        }

        [Fact]
        public void When_Change_Direction_More_Than_Once_Should_Throw_TryChangeSnakeDirectionMoreThenOncePerMoveException()
        {
            // Arrange
            var gameManager = GetGameManagerWithDeafaultParams();

            // Act
            var exception = Record.Exception(() =>
            {
                gameManager.ChangeDirection(Constants.Direction.TOP);
                gameManager.ChangeDirection(Constants.Direction.LEFT);
            });

            // Assert
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<TryChangeSnakeDirectionMoreThenOncePerMoveException>();
        }

        [Fact]
        public void When_Try_Change_Direction_On_Already_Choosen_Should_Throw_WrongDirectionException()
        {
            // Arrange
            var gameManager = GetGameManagerWithDeafaultParams();

            // Act
            var exception = Record.Exception(() =>
            {
                gameManager.ChangeDirection(Constants.Direction.RIGHT);
            });

            // Assert
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<WrongDirectionException>();
        }
        #region ARRANGE
        private readonly ISnakeGameObjectFactory snakeGameObjectFactory
            = new SnakeGameObjectFactory();

        private readonly ISnakeGameManagerFactory snakeGameManagerFactory
            = new SnakeGameManagerFactory();

        private SnakeGameObject GetSnakeInDefaultPosition()
            => snakeGameObjectFactory.CreateSnakeGameObject(new(0, 0));

        private SnakeGameManager GetGameManagerWithDeafaultParams()
            => snakeGameManagerFactory
                .CreateSnakeGameManager(GetSnakeInDefaultPosition(),
                    new Level(new PosXY(10, 10),
                            Enumerable.Empty<PosXY>()));
        #endregion
    }
}
