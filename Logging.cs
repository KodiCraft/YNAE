using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace YNAEmulator
{
    class Logging
    {
        public static string log = "";
        public static Stopwatch stopwatch = new Stopwatch();
        public static void SaveLogs()
        {
            stopwatch.Stop();
            StreamWriter sw = new StreamWriter($"YNAE_{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}A{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}.log");
            sw.Write(log);
            sw.Close();
        }

        public static void StartLogging()
        {
            stopwatch.Start();
        }

        public static void Log(string _log)
        {
            log += "\n";
            log += $"[{stopwatch.ElapsedMilliseconds/1000}] {_log}";
        }
    }
}
