namespace DashboardComm.Logging
{
    /// <summary>
    /// Verbosity level of a given message
    ///
    ///    <para>Critical - Errors that stop execution (0)</para>
    ///       <para>Error - Errors that are recoverable (1)</para>
    ///     <para>Warning - Bad things that happen, but are expected (2)</para>
    /// <para>Information - Updates that might be useful to user (3)</para>
    ///     <para>Verbose - Almost everything (4)</para>
    ///       <para>Debug - Super detail (5)</para>
    /// </summary>

    public enum Verbosity
    {
        Critical,    // Errors that stop execution (0)
        Error,       // Errors that are recoverable (1)
        Warning,     // Bad things that happen, but are expected (2)
        Information, // Updates that might be useful to user (3)
        Verbose,     // Almost Everything (4)
        Debug        // Super detail (5)
    }
}
