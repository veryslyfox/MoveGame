class Cell
{
}
class Empty : Cell
{

}
class Path : Cell
{
    public Path(Cell cell, int pulse, int periodFromPathOpen)
    {
        Cell = cell;
        Pulse = pulse;
        PeriodFromPathOpen = periodFromPathOpen;
    }

    public Cell Cell { get; set; }
    public int Pulse { get; }
    public int PeriodFromPathOpen { get; set; }
}
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
class Tail : Cell
{
    public Tail(Cell cell)
    {
        Cell = cell;
    }

    public Cell Cell { get; }
}
class Generator : Cell
{
    public Generator(Cell cell)
    {
        Cell = cell;
    }

    public Cell Cell { get; }
}
class DirectedGenerator : Cell
{
    public DirectedGenerator(MoveObject moveObject, Direct direct)
    {
        MoveObject = moveObject;
    }

    public MoveObject MoveObject { get; }
}
class MoveObject : Cell
{
    public MoveObject(Direct direct)
    {
        Direct = direct;
    }

    public Direct Direct { get; }
}
enum Direct
{
    Left,
    Right,
    Up,
    Down
}
class Account
{
    public Account(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
    public int Progress { get; private set; }
}