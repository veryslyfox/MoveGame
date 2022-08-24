class LanguageString
{
    public LanguageString(string russian, string english)
    {
        Russian = russian;
        English = english;
    }

    public string Russian { get; }
    public string English { get; }
    public string ToString(Language langaunge)
    {
        string result;
        switch (langaunge)
        {
            case Language.Russian :
                result = Russian;
                break;
            case Language.English :
                result = English;
                break;
            default :
                result = "";
                Debugger.Debug = "langaunge unspecified??";
                break;
        }
        return result;
    }
}