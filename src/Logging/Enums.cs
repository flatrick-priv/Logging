namespace Logging
{
    /* # Links of interest #
     * https://stackoverflow.com/a/5278006/7677629
     * https://docs.microsoft.com/en-us/previous-versions/office/developer/sharepoint-2010/ff604025(v%3Doffice.14)#event-level-guidelines
     */

    /// <summary>
    /// Level of detail for the logging.
    /// </summary>
    public enum LogLevel
    {
        Critical = 0, // Events that demand immediate attention of sysadm
        Error = 1, // Events that indicate problems, should be looked at when appropiate
        Warning = 2, // Events that aren't ideal, but still manageable by the application
        Information = 3, // An information event. This indicates a significant, successful operation.
        Debug = 4, // Events relevant when troubleshooting/debugging
        Trace = 5 // When you absolutely need every single possible detail about what's going on
    }
}