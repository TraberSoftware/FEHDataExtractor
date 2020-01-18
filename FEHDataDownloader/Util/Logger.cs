using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using Colorify;
using Colorify.UI;

namespace FEH_Data_Downloader {
    public static class Logger {
        public enum LOG_LEVEL {
            NONE    = 0,
            ERROR   = 0x2,
            WARNING = 0x4,
            SUCCESS = 0x8,
            INFO    = 0x16,
            DEBUG   = 0x32,

            ALL     = 0x1024
        }
        private  static LOG_LEVEL __LogLevel = 
            LOG_LEVEL.ERROR   | 
            LOG_LEVEL.WARNING | 
            LOG_LEVEL.SUCCESS | 
            LOG_LEVEL.INFO 
        ;

        private static string           __LogBasename          = string.Empty;
        private static string           __LogDirectory         = string.Empty;
        private static string           __LogFilepath          = string.Empty;
        private static string           __LogDate              = string.Empty;
        private static string           __LogBasedir           = "logs";
        private static string           __LogExtension         = "log";

        private static string           __CustomFilename       = string.Empty;

        private static FileStream       __LogStream            = null;
        private static StreamWriter     __LogWriter            = null;
        private static object           __LogWriterLocker      = new { };
        private static Mutex            __MultiprocessLocker   = null;

        private static Colorify.Format  __ColorifyFormat       = new Colorify.Format(Theme.Dark);

        const int LOG_BUFFER_SIZE = 65536;

        public static void SetLogDirectory(string LogPath = "") {
            if(LogPath == "") {
                LogPath = __DefaultLogDirectory();
            }

            if (!Directory.Exists(LogPath)) {
                 Directory.CreateDirectory(LogPath);
            }

            __LogDirectory = LogPath;
        }

        private static string __DefaultLogDirectory() {
            return Path.Combine(
                Util.AddTrailingSlash(Util.ProgramDirectory()),
                __LogBasedir
            );
        }

        public static void SetLogFilebase(string Filebase = "") {
            if(Filebase == string.Empty) {
                Filebase = Util.ProgramName();
            }

            __LogBasename = Filebase;
        }

        private static void __RenderLogFilepath(string TargetLogFilepath = "") {
            string CurrentLogFilepath = __LogFilepath;

            // Custom log filename shall not be rotated (BEWARE!)
            if (__CustomFilename != string.Empty) {
                TargetLogFilepath = Path.Combine(
                    Util.AddTrailingSlash(__LogDirectory),
                    __CustomFilename
                );

                __LogFilepath = TargetLogFilepath;
            }
            else {
                if(__LogDirectory == string.Empty) {
                    SetLogDirectory();
                }

                if(__LogBasename == string.Empty) {
                    SetLogFilebase();
                }

                if (TargetLogFilepath == string.Empty) {
                    __LogDate = Util.CurrentDate("");

                    TargetLogFilepath = Path.Combine(
                        Util.AddTrailingSlash(__LogDirectory),
                        __LogBasename +
                        "_" +
                        __LogDate +
                        "." + 
                        __LogExtension
                    );
                }

                __LogFilepath = TargetLogFilepath;
            }

            if(CurrentLogFilepath != __LogFilepath) {
                __AcknowledgeLogWritter();
            }
        }

        public static string Log(string message, LOG_LEVEL level = LOG_LEVEL.INFO, string callerClass = null, string callerMethod = null) {
            Thread thread = Thread.CurrentThread;
            int thread_id = 0;
            if (thread.IsThreadPoolThread) {
                thread_id = thread.ManagedThreadId;
            }

            MethodBase currentMethod = new System.Diagnostics.StackFrame(1).GetMethod();
            if (callerClass == null) {
                callerClass = currentMethod.Module.ToString();
            }
            if (callerMethod == null) {
                callerMethod = currentMethod.Name;
            }

            if (callerMethod.IndexOf('<') > -1) {
                callerMethod = callerMethod.Substring(
                    callerMethod.IndexOf('<') + 1,
                    callerMethod.IndexOf('>') - 1 - callerMethod.IndexOf('<')
                );
            }

            string log_msg = "" +
                "[" + Util.CurrentTimestamp(true)       + "]" + " " +
                "[" + level                             + "]" + " " +
                "[" + callerClass + "::" + callerMethod + "]" + " " +
                "[" + thread_id                         + "]" + " " +
                message;

            if (
                __LogLevel.HasFlag(level)
                ||
                __LogLevel.Equals(LOG_LEVEL.ALL)
            ) {
                try {
                    __ColorifyFormat.WriteLine(log_msg, __GetColor(level));
                }
                catch {
                    Console.WriteLine(log_msg);
                }
            }

            return log_msg;
        }

        public static void LogToFile(string message, LOG_LEVEL level = LOG_LEVEL.INFO, string callerClass = null, string callerMethod = null) {
            MethodBase currentMethod = new System.Diagnostics.StackFrame(1).GetMethod();
            if (callerClass == null) {
                callerClass = currentMethod.Module.ToString() + "::" + currentMethod.DeclaringType.FullName;
                //callerClass = currentMethod.Module.ToString();
            }
            if (callerMethod == null) {
                callerMethod = currentMethod.Name;
            }

            if (callerClass.IndexOf('+') > -1) {
                callerClass = callerClass.Substring(
                    0,
                    callerClass.IndexOf('+')
                );
            }
            if (callerMethod.IndexOf('<') > -1) {
                callerMethod = callerMethod.Substring(
                    callerMethod.IndexOf('<') + 1,
                    callerMethod.IndexOf('>') - 1 - callerMethod.IndexOf('<')
                );
            }

            string Message = Logger.Log(message, level, callerClass, callerMethod);

            if(Message == string.Empty) {
                return;
            }

            __WriteToLog(Message);
        }

        private static string __GetColor(LOG_LEVEL level) {
            string color = Colors.txtDefault;
            switch (level) {
                case LOG_LEVEL.WARNING:
                    color = Colors.txtWarning;
                    break;
                case LOG_LEVEL.SUCCESS:
                    color = Colors.txtSuccess;
                    break;
                case LOG_LEVEL.INFO:
                    color = Colors.bgDefault;
                    break;
                case LOG_LEVEL.ERROR:
                    color = Colors.txtDanger;
                    break;
                case LOG_LEVEL.DEBUG:
                    color = Colors.txtInfo;
                    break;
            }

            return color;
        }

        private static void __WriteToLog(string input) {
            __RenderLogFilepath();

            if (__LogWriter == null) {
                __AcknowledgeLogWritter();
            }

            try {
                // Multiprocess lock
                while (!__AcquireWriterMutex()) {
                    Thread.Sleep(10);
                }

                // Multithread lock
                lock (__LogWriterLocker) {
                    __LogWriter.WriteLine(input);
                }
            }
            catch (Exception exception) {
                Logger.Log(
                    "Can't write to log file: " + Environment.NewLine +
                    exception.Source            + Environment.NewLine +
                    exception.Message           + Environment.NewLine +
                    exception.StackTrace        + Environment.NewLine,
                    LOG_LEVEL.ERROR
                );
                __RenderLogFilepath();

                __LogWriter.WriteLine(input);
            }
            finally {
                __LogWriter.Flush();

                __ReleaseMultiprocessLocker();
            }
        }

        private static void __AcknowledgeLogWritter() {
            __CreateMultiprocessLocker();

            if (__LogWriter != null) {
                try {
                    __LogWriter.Flush();
                    __LogStream.Flush();

                    __LogWriter.Close();
                    __LogStream.Close();

                    __LogWriter.Dispose();
                    __LogStream.Dispose();
                }
                catch (ObjectDisposedException) { }
            }

            __LogStream = System.IO.File.Open(__LogFilepath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            __LogWriter = new StreamWriter(__LogStream, Encoding.UTF8, LOG_BUFFER_SIZE);
        }

        private static bool __AcquireWriterMutex() {
            bool createdNew;
            __MultiprocessLocker = new Mutex(true, Util.HashString(__LogFilepath), out createdNew);
            if (createdNew) {
                // Mutex was created so ownership is guaranteed; no need to wait on it.
                return true;
            }

            try {
                if (!__MultiprocessLocker.WaitOne(150, false)) {
                    return false;
                }
            }
            catch (AbandonedMutexException) {
                // Other application was aborted, which led to an abandoned mutex.
                // This is fine, as we have still successfully acquired the mutex.
            }

            return true;
        }

        private static void __CreateMultiprocessLocker() {
            if (__MultiprocessLocker != null) {
                __MultiprocessLocker.Dispose();
            }
            __MultiprocessLocker = new Mutex(false, Util.HashString(__LogFilepath), out bool firstInstance);
        }

        private static void __ReleaseMultiprocessLocker() {
            try {
                __MultiprocessLocker.ReleaseMutex();
            }
            catch (System.NullReferenceException) {
                __CreateMultiprocessLocker();
            }
            catch (System.ObjectDisposedException) {
                __CreateMultiprocessLocker();
            }
            catch (System.ApplicationException) { }
        }
    }
}
