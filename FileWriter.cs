class FileWriter
{
    public FileWriter(Stream stream)
    {
        Stream = stream;
    }

    public void Write(string file, string text)
    {
        using var stream = File.Open(file, FileMode.OpenOrCreate);
        stream.Write(AsByte(text));
    }
    public void Write<T>(T[,] values, IStringConverter<T>? converter, string file)
    {
        if (converter == null)
        {
            converter = IStringConverter<T>.Create();
        }
        for (int row = 0; row < values.GetLength(1); row++)
        {
            for (int column = 0; column < values.GetLength(0); column++)
            {
                Write(converter.ToString(values[column, row]) + ",", file);
            }
            Write(",", file);
        }
    }
    static string NewLine { get => "\n\r"; }
    public Stream Stream { get; }

    public static byte[] AsByte(string s)
    {
        var result = new byte[s.Length];
        for (int i = 0; i < s.Length - 1; i++)
        {
            result[i] = (byte)(s[i]);
        }
        return result;
    }
}
interface IStringConverter<T>
{
    string? ToString(T value);
    private class Default : IStringConverter<T>
    {
        public string? ToString(T value)
        {
            return value!.ToString();
        }
    }
    public static IStringConverter<T> Create()
    {
        return new Default();
    }
}