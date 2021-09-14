using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace YNAEmulator
{
    class EmulatorCommands
    {
        public static Dictionary<string, Command> commands = new Dictionary<string, Command>()
        {
            { "help", Help },
            { "h", Help },
            { "?", Help },

            { "run", Run },
            { "r", Run },
        };

        public delegate void Command(string[] args);

        public static void ExecCommand(string[] args)
        {
            Logging.Log($"Got arguments [{string.Join(", ", args)}]");
            try
            {
                commands[args[0]](args.Skip(1).ToArray());
            } catch (KeyNotFoundException)
            {
                Logging.Log($"'{args[0]} isn't a valid argument!'");
                Help(args);
            }
        }

        public static void Help(string[] args)
        {
            Logging.Log("Displaying help...");
            Console.WriteLine("==== YNAE Help ====");
            Console.WriteLine("help - Displays the help menu");
            Console.WriteLine("run <yna file path> [args] - Runs the selected yna file");

            Console.Read();
        }

        public static void Run(string[] args)
        {
            Logging.Log($"Running {args[0]} with arguments [{string.Join(", ", args.Skip(1).ToArray())}]");
            try
            {
                StreamReader reader = new StreamReader(args[0]);
                string _yna = reader.ReadToEnd();
                Logging.Log($"Finished reading all of {args[0]}... beginning emulation!");
                reader.Close();
                EmulatorCore.Start(_yna, args.Skip(1).ToArray());

            } catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERROR: {e.Message}");
                Logging.Log($"Encountered error while reading file: {e.Message}");
            }
        }
    }
}
