class Level
{
    public Level(Cell[,] cells, int index)
    {
        Cells = cells;
        Index = index;
    }
    public Cell[,] Cells { get; }
    public int Index { get; }
    public bool IsPassed { get => Program._currentAccount.Progress >= Index; }
}