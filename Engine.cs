enum Instrument
{
    EmptyCreator,
    GenCreator,
    BombCreator,
    PersonageCreator,

}
static class Engine
{
    static Instrument _instrument;
    static int _x;
    static int _y;
    public static void CreateLevel()
    {
        var size = Console.ReadLine();
        
    }
    public static Level[] CreateLevel(int createNumber)
    {
        return null;
    }
    public static void ProcessLogic()
    {
        var command = Console.ReadLine();
        if (command.StartsWith("create level ", StringComparison.InvariantCultureIgnoreCase))
        {
            FileWriter writer = new FileWriter();
            if (int.TryParse(command.Substring(13), out var result))
            {
                foreach (var item in CreateLevel(result))
                {
                    writer.Write<Cell>(item.Cells, new Converter(), "levels.txt");
                }
            }
        }   
    }
}
struct Converter : IStringConverter<Cell>
{
    public string? ToString(Cell value)
    {
        return Program.ToString(value);
    }
}