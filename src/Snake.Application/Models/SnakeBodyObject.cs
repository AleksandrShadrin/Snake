using Snake.Core.ValueObjects;

namespace Snake.Application.Models
{
    public record SnakeBodyObject(PosXY Head, PosXY? Tail, IEnumerable<PosXY> Body);
}
