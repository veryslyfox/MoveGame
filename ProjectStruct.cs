abstract class Cell
{
}
class Empty : Cell
{

}
// class Path : Cell
// {
//     public Path(Cell cell, int pulse, int periodFromPathOpen)
//     {
//         Cell = cell;
//         Pulse = pulse;
//         PeriodFromPathOpen = periodFromPathOpen;
//     }

//     public Cell Cell { get; set; }
//     public int Pulse { get; }
//     public int PeriodFromPathOpen { get; set; }
// }
class Key : Cell
{
    public Key(Door door)
    {
        Door = door;
    }

    public Door Door { get; }
}
class Door : Cell
{
    public Door(ConsoleColor color)
    {
        Color = color;
    }

    public ConsoleColor Color { get; }
}
// class Tail : Cell
// {
//     public Tail(Cell cell)
//     {
//         Cell = cell;
//     }

//     public Cell Cell { get; }
// }
// class Generator : Cell
// {
//     public Generator(Cell cell)
//     {
//         Cell = cell;
//     }

//     public Cell Cell { get; }
// }
class DirectedGenerator : Cell
{
    public DirectedGenerator(MoveObject moveObject, Direct direct)
    {
        MoveObject = moveObject;
    }

    public MoveObject MoveObject { get; }
}
abstract class MoveObject : Cell
{
    public MoveObject(Direct direct, int moveIntervalTime)
    {
        Direct = direct;
        MoveIntervalTime = moveIntervalTime;
    }

    public Direct Direct { get; }
    public long MoveIntervalTime { get; }
    public long LastMoveTime { get; set; }
    public abstract void Collide(Cell cell);
}
class Personage : Cell
{
}
enum Direct
{
    Left,
    Right,
    Up,
    Down
}
