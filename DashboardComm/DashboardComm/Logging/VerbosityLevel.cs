namespace DashboardComm.Logging
{
    public enum VerbosityLevel
    {
        Debug, // super detail (5)
        Verbose,  // everything (4)
        Information, // updates that might be useful to user (3)
        Warning, // bad things that happen, but are expected (2)
        Error, // errors that are recoverable (1)
        Critical // errors that stop execution (0)
    }
}