class FileReader
{
    public FileReader(Stream stream)
    {
        Stream = stream;
    }
    public string ReadString(int start, int end)
    {
        var buffer = new byte[end - start];
        Stream.Read(buffer, start, end - start);
        var result = "";
        foreach (var item in buffer)
        {
            
        }
    }
    public Stream Stream { get; }
}