class Level
{
    public Level(Cell[,] cells)
    {
        Cells = cells;
    }
    public Level(Cell[,] cells, int personageX, int personageY)
    {
        Cells = cells;
        PersonageX = personageX;
        PersonageY = personageY;
    }
    public Cell[,] Cells { get; }
    public int PersonageX { get; }
    public int PersonageY { get; }
}