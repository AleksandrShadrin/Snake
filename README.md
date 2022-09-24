## Задачи

### 1. Ядро проекта

- Создать игровой объект змейки, который будет иметь состояние жизни, методы для управления этим состоянем, тело змейки (_private list_), методы возвращающие голову, тело и хвост, также методы перемещения и увеличения змейки. Змейка должна уметь определять столкновение своего тела с объектом в точке.

- Создать gameManager для управления игрой, где будет хранится объект змейки (_private snake_), также направление движения змейки, объекты наград и уровень (стенки). В данный объект передаются змейка и уровень. Должны быть методы управления очками, проверки коллизии игровых объектов, перемещение змейки на одну позицию, передачи состояния игры
- Добавить объекты уровня, двумерной точки, награды и другие.

### 2. Создание сервисного проекта, чтобы UI проект не использовал ядро

- Создать сервис игры в змейку, который предоставляет методы создания игры, передача данных из gameManager, таких как уровень, награды и тело змеи (_новый объект, содержит голову, тело и хвост_). Содержит методы создания игры, добавления объектов наград на поле и передвинуть змейку на одну позицию.

### 3. Создать UI

- Создать игровой движок, который запускает игровой цикл в котором происходит отрисовка, перемещение змейки и генерация наград.

- Создать обработчик нажатий.

- Создать сервис для отрисовки объектов.

### 4. Провести модульное тестирование проектов

- Создать тесты для сборки Snake.Core.

- Создать тесты для сборки Snake.Application.

### Выполнено:

1. Ядро проекта сделано, была добавлена дополнительная логика.

2. Сервисы написаны и реализованы.

3. Первоначальный UI был пересмотрен и заменён на более гибкий.

4. Тесты написаны.
