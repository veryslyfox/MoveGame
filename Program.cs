using System.Diagnostics;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

static class Program
{
#pragma warning disable
    static Cell[,] _field;
    static AccountRecord _account;
    static Language _language;
    static int _personageX;
    static int _personageY;
    static bool _onEngine;
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
                if (_onEngine)
                {
                    Next();
                    ProcessInput();
                    DrawField();
                }
                else
                {
                    Engine.ProcessLogic();
                }
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
                    if (GetCurrentTime() - moveObject.LastMoveTime >= moveObject.MoveIntervalTime)
                    {
                        moveObject.LastMoveTime = GetCurrentTime();
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
        var stream = File.Open("levels.txt", FileMode.Open);
        var writer = new FileWriter();
        var reader = new FileReader();
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
            _account = new AccountRecord(command.Substring(15), age, _account.Progress);
        }
        if (StartsWith("переименовать аккаунт "))
        {
            _account = new AccountRecord(command.Substring(21), age, _account.Progress);
        }
        if (StartsWith("add account "))
        {
            writer.Write("accounts.txt", command.Substring(12) + "," + age);
        }
        if (StartsWith("добавить аккаунт "))
        {
            writer.Write("accounts.txt", command.Substring(17) + "," + age);
        }
        if (command == "on engine")
        {
            _onEngine = true;
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
    static long GetCurrentTime()
    {
        var time = Stopwatch.GetTimestamp();
        return time;
    }
    public static string ToString(Cell cell)
    {
        if (cell is Empty empty)
        {
            return "empty";
        }
        if (cell is DirectedGenerator directedGenerator)
        {
            return $"generator({ToString(directedGenerator.MoveObject)})";
        }
        Debugger.Problem = "new cell type";
        Debugger.ProblemFile = "Program.cs";
        Debugger.ProblemString = 231;
        return "";
    }
    static void DrawField()
    {
        var level = LevelCompiler.GetLevel(_account.Progress);
        Console.Write(Debugger.Problem);
    }
}
