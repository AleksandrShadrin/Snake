using Shouldly;
using Snake.Application.Adapters;
using Snake.Core.Factories;
using Snake.Core.ValueObjects;

namespace Snake.Application.Tests
{
    public class SnakeGameServiceTests
    {
        [Fact]
        public void When_Snake_Hit_Reward_Object_Score_Should_Be_Increased()
        {
            // Arrange
            var level = new Level(new PosXY(20, 20), Enumerable.Empty<PosXY>());
            var startPos = new PosXY(0,0);
            var rewardObject = new RewardObject(new PosXY(1, 0), 4);
            var snakeGameService = CreateSnakeGameService();

            // Act
            snakeGameService.CreateGame(startPos, level);
            snakeGameService.AddRewardObject(rewardObject);
            snakeGameService.MoveSnake();

            // Assert
            snakeGameService.GetRewards().Count.ShouldBe(0);
            snakeGameService.GetScore().ShouldBe<uint>(4);
        }

        [Fact]
        public void When_Change_Direction_From_Right_To_Bottom_Next_Snake_Position_Should_Be_X_Is_0_Y_Is_1()
        {
            // Arrange
            var level = new Level(new PosXY(20, 20), Enumerable.Empty<PosXY>());
            var startPos = new PosXY(0, 0);
            var snakeGameService = CreateSnakeGameService();

            // Act
            snakeGameService.CreateGame(startPos, level);
            snakeGameService.ChangeMoveDirection(Core.Constants.Direction.BOTTOM);
            snakeGameService.MoveSnake();

            // Assert
            snakeGameService.GetSnakeBody().Head.ShouldBe(new PosXY(0, 1));
        }

        [Fact]
        public void When_Snake_Hit_The_Wall_It_Should_Be_Dead_And_Game_Be_Is_Over()
        {
            // Arrange
            var level = new Level(new PosXY(20, 20), new List<PosXY> { new PosXY(1,0)});
            var startPos = new PosXY(0, 0);
            var snakeGameService = CreateSnakeGameService();

            // Act
            snakeGameService.CreateGame(startPos, level);
            snakeGameService.MoveSnake();

            // Assert
            snakeGameService.GameIsOver().ShouldBeTrue();
        }

        [Fact]
        public void Remove_Reward_Should_Work_Correctly()
        {
            // Arrange
            var level = new Level(new PosXY(20, 20), Enumerable.Empty<PosXY>());
            var startPos = new PosXY(0, 0);
            var rewardObject = new RewardObject(new PosXY(1, 0), 4);
            var snakeGameService = CreateSnakeGameService();

            // Act
            snakeGameService.CreateGame(startPos, level);
            snakeGameService.AddRewardObject(rewardObject);
            var rewardsCountBeforeRemove = snakeGameService.GetRewards().Count();
            snakeGameService.RemoveRewardObject(rewardObject);
            var rewardsCountAfterRemove = snakeGameService.GetRewards().Count();

            // Assert
            rewardsCountBeforeRemove.ShouldBe(1);
            rewardsCountAfterRemove.ShouldBe(0);
        }

        [Fact]
        public void GetWalls_Method_Should_Work_Correctly()
        {
            // Arrange
            var walls = new List<PosXY> { new(1,1), new(1, 2), new(1, 3) };
            var level = new Level(new PosXY(20, 20), walls);
            var startPos = new PosXY(0, 0);
            var snakeGameService = CreateSnakeGameService();

            // Act
            snakeGameService.CreateGame(startPos, level);
            var wallsFromMethod = snakeGameService.GetWalls().ToList();

            // Assert
            walls.ShouldBeEquivalentTo(wallsFromMethod);
        }
        #region ARRANGE
        private readonly ISnakeGameManagerFactory snakeGameManagerFactory
            = new SnakeGameManagerFactory();

        private readonly ISnakeGameObjectFactory snakeGameObjectFactory
            = new SnakeGameObjectFactory();

        private ISnakeGameService CreateSnakeGameService()
            => new SnakeGameService( snakeGameManagerFactory, snakeGameObjectFactory);
        #endregion
    }
}
