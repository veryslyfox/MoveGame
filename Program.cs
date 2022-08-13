static class Program
{
    public static Cell[,] _field;
    public static Account _currentAccount;

    static void Main()
    {
        while (true)
        {
        }
    }
    static void Next()
    {
        for (int row = 0; row < _field.GetLength(1); row++)
        {
            for (int column = 0; column < _field.GetLength(0); column++)
            {
                var cell = _field[column, row];
                if (cell is Path path)
                {
                    var pathcell = path.Cell;
                    path.PeriodFromPathOpen -= 1;
                    if (pathcell.GetType() == typeof(Tail))
                    {
                        pathcell = new Cell();
                    }
                    else if (path.Cell != new Cell())
                    {
                        pathcell = new Tail(pathcell);
                    }
                    _field[column, row] = path;
                    path.Cell = pathcell;
                }
                if (cell is Generator generator)
                {
                    NeighborhoodActive((Cell param) =>
                     {
                         if (param is Path path && path.Cell == generator.Cell)
                         {
                             param = generator.Cell;
                        }

                     }, column, row);
                }
                if (cell is MoveObject moveObject)
                {
                    if (moveObject.Direct == Direct.Left)
                    {
                        
                    }
                }
            }
        }
    }
    static void NeighborhoodActive(Action<Cell> action, int column, int row)
    {
        void ActionFromCell(int ca, int ra)
        {
            action(_field[column + ca, row + ra]);
        }
        var width = _field.GetLength(0);
        var height = _field.GetLength(1);
        if (row > 0 && column > 0)
        {
            ActionFromCell(-1, -1);
        }

        if (row > 0)
        {
            ActionFromCell(0, -1);
        }

        if (row > 0 && column < width - 1)
        {
            ActionFromCell(1, -1);
        }

        if (column < width - 1)
        {
            ActionFromCell(1, 0);
        }

        if (column < width - 1 && row < height - 1)
        {
            ActionFromCell(1, 1);
        }

        if (row < height - 1)
        {
            ActionFromCell(0, 1);
        }

        if (row < height - 1 && column > 0)
        {
            ActionFromCell(-1, 1);
        }

        if (column > 0)
        {
            ActionFromCell(-1, 0);
        }
    }
    static void ProcessInput()
    {

    }
}