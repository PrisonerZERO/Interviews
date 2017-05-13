//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Common.Diagnostics
{
    using System;
    using System.Collections.Concurrent;
    using System.Diagnostics;
    using System.Globalization;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;

    //<TraceLevels>
    //    Off = 0
    //    Error = 1
    //    Warning = 2
    //    Info = 3
    //    Verbose = 4
    //</TraceLevels>

    /// <summary> An efficient diagnostics logging handler</summary>
    /// <note>
    ///     This class should be used in conjunction with a targeted logger. Using this wrapper allows you to 
    ///     switch to another logger easily...and you dont have to change any of the consuming code to do so.
    ///     
    ///         AppendToPhysicalLog
    ///         - Aspects of this method would change depending on which Logger you are using (Log4Net is just an example)
    ///</note>
    public static class TraceHandler
    {
        #region <Fields>

        private const string TAB = "\t";

        private const string TRACE_UNKNOWN = "Unknown";
        private const string TRACE_TRACESWITCH_DESCRIPTION = "This TraceSwitch is used to control which information to trace at runtime (Info, Debug, Warn, Error).";
        private const string TRACE_OUT = "OUT:";
        private const string TRACE_IN = "IN:";
        private const string TRACE_TRACEOUT_FORMAT = "{0} {1}";
        private const string TRACE_TRACEIN_FORMAT = "{0} {1}";
        private const string TRACE_METHODNAME_FORMAT = "[{0}.{1}]";

        // EXAMPLE: Let's say you wanted - Log4Net
        //private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static ConcurrentDictionary<string, StringBuilder> memoryLogs;

        private static TraceLevel CurrentTraceLevel = TraceLevel.Off;

        #endregion

        #region <Constructors>

        static TraceHandler()
        {
            TraceSwitch = new TraceSwitch("TraceHandler.TraceSwitch.Level", TRACE_TRACESWITCH_DESCRIPTION);
        }

        #endregion

        #region <Properties>

        public static TraceSwitch TraceSwitch { get; private set; }

        #endregion

        #region <Methods>
        
        #region public

        /// <summary>Traces-in the calling method using the TraceSwitch.Level value located in the callers configuration file.</summary>
        public static void TraceIn()
        {
            var callingMethod = GetCallingMethod();
            var methodFullname = GetMethodFullname(callingMethod);

            CurrentTraceLevel = TraceLevel.Info;
            TraceIn(methodFullname);
        }

        /// <summary>Traces-in the calling method using the TraceSwitch.Level value passed-into the method.</summary>
        public static void TraceIn(TraceLevel level)
        {
            var callingMethod = GetCallingMethod();
            var methodFullname = GetMethodFullname(callingMethod);

            CurrentTraceLevel = level;
            TraceIn(methodFullname);
        }

        /// <summary>Traces-in the calling method using the TraceSwitch.Level value passed-into the method.</summary>
        /// <exception cref="ArgumentNullException">Non-Existent 'MethodFullname' property throws this exception</exception>
        public static void TraceIn(TraceLevel level, string methodFullname)
        {
            if (string.IsNullOrEmpty(methodFullname))
                throw new ArgumentNullException("MethodFullname");

            CurrentTraceLevel = level;
            TraceIn(methodFullname);
        }

        /// <summary>Appends a formatted value.</summary>
        /// <note>Does not write when TraceSwitch.Level is Off.</note>
        /// <exception cref="ArgumentNullException">Non-Existent 'Format' property throws this exception</exception>
        /// <exception cref="ArgumentNullException">Non-Existent 'Parameters' property throws this exception</exception>
        public static void TraceAppend(string format, params object[] parameters)
        {
            if (string.IsNullOrEmpty(format))
                throw new ArgumentNullException("Format");

            if (parameters == null)
                throw new ArgumentNullException("Parameters");

            var callingMethod = GetCallingMethod();
            var formatted = GetFormattedString(format, parameters);

            AppendInformationToMemoryLog(callingMethod, formatted);
        }

        /// <summary>Appends a message.</summary>
        /// <note>Does not write when TraceSwitch.Level is Off.</note>
        /// <exception cref="ArgumentNullException">Non-Existent 'Message' property throws this exception</exception>
        public static void TraceAppend(string message)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException("Message");

            var callingMethod = GetCallingMethod();

            AppendInformationToMemoryLog(callingMethod, message);
        }

        /// <summary>Appends a formatted value.</summary>
        /// <note>Does not write when TraceSwitch.Level is Off.</note>
        /// <exception cref="ArgumentNullException">Non-Existent 'Format' property throws this exception</exception>
        /// <exception cref="ArgumentNullException">Non-Existent 'Parameters' property throws this exception</exception>
        public static void TraceWarning(string format, params object[] parameters)
        {
            if (string.IsNullOrEmpty(format))
                throw new ArgumentNullException("Format");

            if (parameters == null)
                throw new ArgumentNullException("Parameters");

            var callingMethod = GetCallingMethod();
            var formatted = GetFormattedString(format, parameters);

            CurrentTraceLevel = TraceLevel.Warning;
            AppendWarningToMemoryLog(callingMethod, formatted);
        }

        /// <summary>Appends a message.</summary>
        /// <note>Does not write when TraceSwitch.Level is Off.</note>
        /// <exception cref="ArgumentNullException">Non-Existent 'Message' property throws this exception</exception>
        public static void TraceWarning(string message)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException("Message");

            var callingMethod = GetCallingMethod();

            CurrentTraceLevel = TraceLevel.Warning;
            AppendWarningToMemoryLog(callingMethod, message);
        }

        /// <summary>Appends a formatted value.</summary>
        /// <note>Overrides the current TraceSwitch.Level and sets it to Error.</note>
        /// <exception cref="ArgumentNullException">Non-Existent 'Format' property throws this exception</exception>
        /// <exception cref="ArgumentNullException">Non-Existent 'Parameters' property throws this exception</exception>
        public static void TraceError(string format, params object[] parameters)
        {
            if (string.IsNullOrEmpty(format))
                throw new ArgumentNullException("Format");

            if (parameters == null)
                throw new ArgumentNullException("Parameters");

            var callingMethod = GetCallingMethod();
            var formatted = GetFormattedString(format, parameters);

            CurrentTraceLevel = TraceLevel.Error;
            AppendErrorToMemoryLog(callingMethod, formatted);
        }

        /// <summary>Appends a message.</summary>
        /// <note>Overrides the current TraceSwitch.Level and sets it to Error.</note>
        /// <exception cref="ArgumentNullException">Non-Existent 'Message' property throws this exception</exception>
        public static void TraceError(string message)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentException("String is null or empty.", "Message");

            var callingMethod = GetCallingMethod();

            CurrentTraceLevel = TraceLevel.Error;
            AppendErrorToMemoryLog(callingMethod, message);
        }

        /// <summary>Appends an exception.</summary>
        /// <note>Overrides the current TraceSwitch.Level and sets it to Error.</note>
        /// <exception cref="ArgumentNullException">Non-Existent 'Exception' property throws this exception</exception>
        public static void TraceError(Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException("Exception");

            var callingMethod = GetCallingMethod();

            CurrentTraceLevel = TraceLevel.Error;
            AppendErrorToMemoryLog(callingMethod, exception.ToString());
        }

        /// <summary>Appends set of values to the physical log.</summary>
        /// <note>Does not write when TraceSwitch.Level is Off.</note>
        public static void TraceOut()
        {
            var callingMethod = GetCallingMethod();
            var methodFullname = GetMethodFullname(callingMethod);
            var key = GetMemoryLogKey(methodFullname);
            var ignored = new StringBuilder();

            if (TraceSwitch.Level == TraceLevel.Off)
            {
                memoryLogs.TryRemove(key, out ignored);
                return;
            }
            
            var functionLog = GetMemoryLog(key);

            var format = string.Format(CultureInfo.CurrentCulture, TRACE_TRACEOUT_FORMAT, TRACE_OUT, callingMethod);
            var message = ApplyTabbing(1, format);

            functionLog.AppendLine(message);

            AppendToPhysicalLog(functionLog.ToString());

            // NOTE: There is no single-parameter overload for TryRemove
            memoryLogs.TryRemove(key, out ignored);
        }

        #endregion

        #region private

        private static void TraceIn(string methodFullname)
        {
            var key = GetMemoryLogKey(methodFullname);
            var functionLog = GetMemoryLog(key);

            var format = string.Format(TRACE_TRACEIN_FORMAT, TRACE_IN, methodFullname);
            var message = ApplyTabbing(1, format);

            functionLog.Append(Environment.NewLine);
            functionLog.AppendLine(message);
            functionLog.Append(Environment.NewLine);
        }

        private static void AppendInformationToMemoryLog(MethodBase callingMethod, string message)
        {
            var methodFullname = GetMethodFullname(callingMethod);
            var key = GetMemoryLogKey(methodFullname);

            AppendToMemoryLog(key, message);
        }

        private static void AppendWarningToMemoryLog(MethodBase callingMethod, string message)
        {
            var methodFullname = GetMethodFullname(callingMethod);
            var key = GetMemoryLogKey(methodFullname);

            AppendToMemoryLog(key, message);
        }

        private static void AppendErrorToMemoryLog(MethodBase callingMethod, string message)
        {
            // NOTE: Errors always override the current TraceLevel
            if (TraceSwitch.Level < TraceLevel.Error)
                TraceSwitch.Level = TraceLevel.Error;

            var methodFullname = GetMethodFullname(callingMethod);
            var key = GetMemoryLogKey(methodFullname);

            AppendToMemoryLog(key, message);
        }

        private static void AppendToMemoryLog(string key, string message)
        {
            var functionLog = GetMemoryLog(key);
            var text = ApplyTabbing(2, message);

            functionLog.AppendLine(text);
            functionLog.Append(Environment.NewLine);
        }

        private static void AppendToPhysicalLog(string message)
        {
            var category = "None";
            var matches = Regex.Match(message, @"IN: \[(.*?)\]") as Match;

            if (matches.Groups.Count > 0)
                category = matches.Groups[1].Value.Length == 0 ? "Category Unknown" : matches.Groups[1].Value;

            // INFO
            if (CurrentTraceLevel == TraceLevel.Info || CurrentTraceLevel == TraceLevel.Verbose)
            {
                Trace.TraceInformation(message);

                if (TraceSwitch.Level >= TraceLevel.Info || TraceSwitch.Level == TraceLevel.Verbose)
                {
                    // INSERT CUSTOM LOG-CALL HERE

                    // EXAMPLE
                    // Log4Net: Info Level = 40000
                    //if (log.IsInfoEnabled)
                    //    log.Info(message);
                }
            }

            // WARNING
            else if (CurrentTraceLevel == TraceLevel.Warning)
            {
                Trace.TraceWarning(message);

                if (TraceSwitch.Level >= TraceLevel.Warning)
                {
                    // INSERT CUSTOM LOG-CALL HERE

                    // EXAMPLE
                    // Log4Net: Debug Level = 30000
                    //if (log.IsDebugEnabled)
                    //    log.Debug(message);

                    // Log4Net: Warn Level = 60000
                    //else if (log.IsWarnEnabled)
                    //    log.Warn(message);
                }
            }

            // ERROR
            else if (CurrentTraceLevel == TraceLevel.Error)
            {
                Trace.TraceError(message);

                // INSERT CUSTOM LOG-CALL HERE

                // EXAMPLE
                // Log4Net: Error Level = 70000
                //if (log.IsErrorEnabled)
                //    log.Error(message);

                // Log4Net: Fatal Level = 110000
                //else if (log.IsFatalEnabled)
                //    log.Fatal(message);
            }
        }

        private static string ApplyTabbing(int tabbingDepth, string message)
        {
            if (tabbingDepth == 0)
                return message;

            var tabbing = GetTabbing(tabbingDepth);

            return string.Concat(tabbing, message);
        }

        private static string GetTabbing(int tabbingDepth)
        {
            var tabbing = string.Empty;

            if (tabbingDepth == 0)
                return tabbing;

            for (int i = 0; i < tabbingDepth; i++)
                tabbing += TAB;

            return tabbing;
        }

        private static string GetFormattedString(string format, params object[] parameters)
        {
            if (((parameters != null) && (parameters.Length > 0)))
                return string.Format(format, parameters);

            return format;
        }

        private static string GetMethodFullname(MethodBase callingMethod)
        {
            if ((callingMethod == null) || (callingMethod.DeclaringType == null))
                return TRACE_UNKNOWN;

            return string.Format(CultureInfo.InvariantCulture, TRACE_METHODNAME_FORMAT, new object[] { callingMethod.DeclaringType.Name, callingMethod.Name });
        }
        private static MethodBase GetCallingMethod()
        {
            var frames = new StackTrace().GetFrames();

            if(frames[2] != null)
            {
                var method = new StackTrace().GetFrame(2).GetMethod();
                var methodName = method.Name;
                var className = method.ReflectedType.Name;
            }
                
            return frames == null || frames.Length < 3 ? null : frames[2].GetMethod();
        }

        private static string GetMemoryLogKey(string callingMethod)
        {
            return callingMethod + "." + Thread.CurrentThread.ManagedThreadId;
        }

        private static StringBuilder GetMemoryLog(string key)
        {
            if (memoryLogs == null)
                memoryLogs = new ConcurrentDictionary<string, StringBuilder>();

            if (!memoryLogs.ContainsKey(key))
            {
                var funcLog = new StringBuilder();
                memoryLogs.TryAdd(key, funcLog);
            }

            StringBuilder result = null;

            if(memoryLogs.ContainsKey(key))
                result = memoryLogs[key];

            if (result == null)
                result = new StringBuilder();

            return result;
        }

        #endregion

        #endregion
    }
}