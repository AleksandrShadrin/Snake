namespace Snake.Core.ValueObjects
{
    public record Level(PosXY GameSize, IEnumerable<PosXY> Walls);
}