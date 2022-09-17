using Microsoft.VisualBasic.FileIO;
using Snake.Application.Adapters;
using Snake.Core.Factories;
using Snake.Core.ValueObjects;
using Snake.Presentation;
using Snake.Presentation.Services;

ISnakeGameObjectFactory snakeGameObjectFactory = new SnakeGameObjectFactory();
ISnakeGameManagerFactory gameManagerFactory = new SnakeGameManagerFactory();
ISnakeGameService gameService = new SnakeGameService(gameManagerFactory, snakeGameObjectFactory);
IGameSnakeRenderService render = new ConsoleGameSnakeRenderService();

var level = new Level(new(20, 40), new List<PosXY>() { });
var startPos = new PosXY(5, 5);
var gameEngine = new GameEngine(gameService, render);
gameEngine.Start(startPos, level);
