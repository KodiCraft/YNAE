using System;

namespace YNAEmulator
{
    class Program
    {
        public const string VERNUMBER = "ALPHA 0.0";
        public const string FLUFFVERNAME = "Generations";

        static void Main(string[] args)
        {
            Logging.StartLogging();
            Logging.Log($"Starting YNAE '{FLUFFVERNAME}'... ({VERNUMBER})");
            if(args.Length == 0)
            {
                EmulatorCommands.Help(new string[0]);
            } else
            {
                EmulatorCommands.ExecCommand(args);
            }

            Logging.SaveLogs();
        }
    }
}
