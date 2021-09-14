using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace YNAEmulator
{
    static class EmulatorCore
    {
        static TokenAreaType area = TokenAreaType.STRING;
        static int areaLength = 0;
        static int nestedAreas = 0;
        static List<Symbol> symbols = new List<Symbol>();

        static Dictionary<string, string> variables = new Dictionary<string, string>();


        static public void Start(string code, string[] args)
        {
            char[] tokens = code.ToCharArray();
            Logging.Log("Beginning yna parse...");
            area = TokenAreaType.STRING;
            for(int i = 0; i < tokens.Length; i++)
            {
                //Logging.Log($"Found character '{tokens[i]}'");
                Tokenise(tokens[i], i);
            }
        }

        static void Tokenise(char token, int index)
        {
            #region Current area is of type comment?
            if (area == TokenAreaType.COMMENT)
            {
                if (token != '}')
                {
                    //Logging.Log($"'{token}' is in a comment");
                    symbols.Add(new Symbol(TokenAreaType.COMMENT, token));
                    if (symbols.Last().character == '#')
                    {
                        Logging.Log($"Found '#' as previous character!");
                        string _emuflag = "";
                        for (int i = symbols.Count - areaLength; i < symbols.Count; i++)
                        {
                            symbols[i] = new Symbol(TokenAreaType.EMUFLAG, symbols[i].character);
                            _emuflag += symbols[i].character;
                        }
                        Logging.Log($"Marked current comment as emulator flags.");
                        RunEmulatorFlag(_emuflag);
                    }

                    areaLength++;
                } else
                {
                    Logging.Log($"'{token}' closes the comment.");
                    symbols.Add(new Symbol(TokenAreaType.COMMENT, token));

                    area = TokenAreaType.STRING;
                    areaLength = 1;
                }
            } else
            #endregion
            #region Current area is of type string?
            if (area == TokenAreaType.STRING)
            {
                if (token == '{')
                {
                    Logging.Log($"'{token}' opens a yna tag!");
                    symbols.Add(new Symbol(TokenAreaType.YNA, token));
                    area = TokenAreaType.YNA;
                    nestedAreas++;
                    areaLength = 1;
                }
                else
                {
                    // Logging.Log($"'{token}' is just a character");
                    symbols.Add(new Symbol(TokenAreaType.STRING, token));
                    
                    area = TokenAreaType.STRING;
                    areaLength++;
                }
            } else
            #endregion
            #region Current area is of type yna?
            if (area == TokenAreaType.YNA)
            {
                if (token == '}')
                {
                    Logging.Log($"'{token}' closes the yna tag!");
                    symbols.Add(new Symbol(TokenAreaType.YNA, token));
                    nestedAreas--;
                    if (nestedAreas == 0)
                    {
                        area = TokenAreaType.STRING;
                        areaLength = 1;
                    } else
                    {
                        areaLength++;
                    }
                } else if (token == '{')
                {
                    Logging.Log($"'{token}' opens a new yna tag!");
                    symbols.Add(new Symbol(TokenAreaType.YNA, token));
                    nestedAreas++;
                    areaLength++;
                }
                else if (symbols[index-1].character == '{' && token == '!')
                {
                    Logging.Log($"'{token}' marks this tag as actually a comment!");
                    symbols[index - 1] = new Symbol(TokenAreaType.COMMENT, symbols[index - 1].character);
                    symbols.Add(new Symbol(TokenAreaType.COMMENT, token));
                    area = TokenAreaType.COMMENT;
                    areaLength = 2;
                } else
                {
                    // Logging.Log($"'{token}' is a yna character");
                    symbols.Add(new Symbol(TokenAreaType.YNA, token));

                    areaLength++;
                }
            }
            #endregion

        }
        #region Emulator Flag stuff


        static Dictionary<string, EmulatorFlag> emulatorFlags = new Dictionary<string, EmulatorFlag>()
        {
            { "CREATEVAR", CreateVar }
        };

        delegate void EmulatorFlag(string[] args);

        static void RunEmulatorFlag(string flag)
        {
            Logging.Log($"Reading emulator flag '{flag}'");
            // The flag comes in the format '{!FLAG value1 value2 ... #}'
            // We need to parse that first into the actual flag and a list of arguments

            flag = flag.TrimStart('{').TrimStart('!').TrimEnd('}').TrimEnd('#');

            string[] parts = flag.Split(" ");

            try
            {
                emulatorFlags[parts[0]](parts.Skip(1).ToArray());
            } catch(KeyNotFoundException)
            {
                Logging.Log($"Emulator flag '{parts[0]}' could not be found! Check your spelling?");
            }
        }

        static void CreateVar(string[] args)
        {
            if (args.Length == 2)
            {
                variables[args[0]] = args[1];
                Logging.Log($"Variable '{args[0]}' set to '{args[1]}'");
            } else
            {
                Logging.Log($"Emulator flag 'CREATEVAR' takes 2 arguments, got {args.Length}.");
            }
            
        }
        #endregion
    }

    public struct Symbol
    {
        public TokenAreaType type;
        public char character;

        public Symbol(TokenAreaType _type, char _character)
        {
            type = _type;
            character = _character;
        }
    }

    public enum TokenAreaType
    {
        EMUFLAG = -2,
        COMMENT = -1,
        STRING = 0,
        YNA = 1,
        VARIALBE = 2,
        FUNCTION = 3,
        ARGUMENT = 4,
    }
}
