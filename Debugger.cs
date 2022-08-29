delegate void DebugCall(string problem, string ProblemFile, int problemString);
static class Debugger
{
    public static string Problem = ""; 
    public static string ProblemFile = "";
    public static int ProblemString;
    static event DebugCall Debug;
    public static void Call()
    {
        Debug.Invoke(Problem, ProblemFile, ProblemString);
    }
}