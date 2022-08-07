static class Program
{
    static Cell[,] _field;
    static void Main()
    {
        
    }
    static void Next()
    {
        for (int row = 0; row < _field.GetLength(1); row++)
        {
            for (int column = 0; column < _field.GetLength(0); column++)
            {
                var cell = _field[column, row];
                if (cell is EmptyPath path)
                {
                    path.PeriodFromPathOpen -= 1;
                    path.Cell = null;
                    cell = path;
                    continue;
                }
            }
        }
    }
    static void NeighborhoodActive
    static void ProcessInput()
    {

    }
}