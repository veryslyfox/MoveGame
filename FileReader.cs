class FileReader
{
    public string ReadString(string file, int start, int end)
    {
        var stream = File.Open(file, FileMode.Open);
        var buffer = new byte[end - start];
        stream.Read(buffer, start, end - start);
        var result = "";
        foreach (var item in buffer)
        {
            result += (char)item;
        }
        return result;
    }
}