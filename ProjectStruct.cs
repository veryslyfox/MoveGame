
class Cell
{
}
class Empty : Cell
{

}
class EmptyPath : Cell
{
    public EmptyPath(Cell cell, int pulse, int periodFromPathOpen)
    {
        Cell = cell;
        Pulse = pulse;
        PeriodFromPathOpen = periodFromPathOpen;
    }

    public Cell? Cell { get; set; }
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