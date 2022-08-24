using System.Diagnostics;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

static class Program
{
#pragma warning disable
    public static Cell[,] _field;
    public static AccountRecord _account;
    static Language _language;
    static int _personageX;
    static int _personageY;
#pragma warning restore
    static void Main()
    {
        try
        {
            _language = Language.English;
            _personageX = 0;
            _personageY = 0;
            while (true)
            {
                Next();
                ProcessInput();
                DrawField();
            }
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception);
            Console.ReadLine();
        }
    }
    static Cell[,] GetLevelCells(int index)
    {
        return LevelCompiler.GetLevel(index).Cells;
    }
    static void Next()
    {
        for (int row = 0; row < _field.GetLength(1) - 1; row++)
        {
            for (int column = 0; column < _field.GetLength(0) - 1; column++)
            {
                var cell = _field[column, row];
                if (cell is MoveObject moveObject)
                {
                    if (GetTime(moveObject) - moveObject.LastMoveTime >= moveObject.MoveIntervalTime)
                    {
                        if (moveObject.Direct == Direct.Left && column == 0)
                        {
                            var v = _field[--column, row];
                            if (v != new Empty())
                            {
                                moveObject.Collide(cell);
                            }
                            _field[--column, row] = _field[column, row];
                        }
                        if (moveObject.Direct == Direct.Right && column == _field.GetLength(0) - 1)
                        {
                            var v = _field[++column, row];
                            if (v != new Empty())
                            {
                                moveObject.Collide(cell);
                            }
                            _field[++column, row] = _field[column, row];
                        }
                        if (moveObject.Direct == Direct.Up && column == 0)
                        {
                            var v = _field[column, --row];
                            if (v != new Empty())
                            {
                                moveObject.Collide(cell);
                            }
                            _field[column, --row] = _field[column, row];
                        }
                        if (moveObject.Direct == Direct.Down && column == _field.GetLength(1) - 1)
                        {
                            var v = _field[--column, row];
                            if (v != new Empty())
                            {
                                moveObject.Collide(cell);
                            }
                            _field[--column, row] = _field[column, row];
                        }
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
    static void CommandStringExecution()
    {
        var command = Console.ReadLine();
        var command2 = Console.ReadLine();
        var age = int.Parse(command2);
        bool StartsWith(string commandStart)
        {
            return command.StartsWith(commandStart, StringComparison.InvariantCultureIgnoreCase);
        }
        if (command == null || command2 == null)
        {
            return;
        }
        if (StartsWith("rename account "))
        {
            _account = new(command.Substring(15), age, _account.Progress);
        }
        if (StartsWith("переименовать аккаунт "))
        {
            _account = new(command.Substring(22), age, _account.Progress);
        }
        if (StartsWith("add account "))
        {
            AddAccount(command.Substring(12), age);
        }
        if (StartsWith("добавить аккаунт "))
        {
            AddAccount(command.Substring(17), age);
        }
    }
    static void ProcessInput()
    {
        switch (Console.ReadKey(true).Key)
        {
            case ConsoleKey.C:
                CommandStringExecution();
                break;
        }
    }
    static void AddAccount(string name, int age)
    {
        WriteCsvRecord(new AccountRecord(name, age, 0), "accounts");
    }
    static AccountRecord ReadAccount(int index)
    {
        return ReadCsvRecord<AccountRecord>("accounts", index);
    }
    static void WriteLevel(Level level)
    {
        WriteCsvRecord(level, "levels");
    }
    static Level ReadLevel(int index)
    {
        return ReadCsvRecord<Level>("levels", index);
    }
    static void WriteCsvRecord<T>(T record, string name)
    {
        using var textWriter = new StreamWriter(File.Open(name + ".csv", FileMode.Append, FileAccess.Write), Encoding.UTF8);
        using (var writer = new CsvWriter(textWriter, System.Globalization.CultureInfo.InvariantCulture, leaveOpen: true))
        {
            writer.WriteHeader<T>();
            writer.WriteRecord(record);
        }
        textWriter.WriteLine();
    }
    static T ReadCsvRecord<T>(string name, int index)
    {
        using var textReader = new StreamReader(File.Open(name + ".csv", FileMode.Open, FileAccess.Read), Encoding.UTF8);
        using (var reader = new CsvReader(textReader, System.Globalization.CultureInfo.InvariantCulture, leaveOpen: true))
        {
            var info = reader.GetRecords<T>();
            var record = info.ElementAt(index);
            return record;
        }
    }
    
    public sealed record AccountRecord(string Name, int Age, int Progress);
    static long GetTime(MoveObject moveObject)
    {
        var time = Stopwatch.GetTimestamp();
        moveObject.LastMoveTime = time;
        return time;
    }
    static string ToString(Cell cell)
    {
        if (cell is Empty empty)
        {
            return "empty";
        }
        if (cell is DirectedGenerator directedGenerator)
        {
            return $"generator({ToString(directedGenerator.MoveObject)})";
        }
        Debugger.Debug = "new cell type??";
        return "";
    }
    static void DrawField()
    {
        Console.Write(Debugger.Debug);
    }
}
