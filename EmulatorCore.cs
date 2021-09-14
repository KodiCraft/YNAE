using System;
using System.Collections.Generic;
using System.Text;

namespace YNAEmulator
{
    static class EmulatorCore
    {
        static TokenAreaType area = TokenAreaType.STRING;
        static int nestedAreas = 0;
        static public void Start(string code, string[] args)
        {
            char[] tokens = code.ToCharArray();
        }

        static void Tokenise(char token)
        {
            if(area != TokenAreaType.COMMENT)
            {

            }
        }
    }

    public struct Symbol
    {
        TokenAreaType type;
        char character;

        public Symbol(TokenAreaType _type, char _character)
        {
            type = _type;
            character = _character;
        }
    }

    public enum TokenAreaType
    {
        COMMENT = -1,
        STRING = 0,
        YNA = 1,
        VARIALBE = 2,
        FUNCTION = 3,
        ARGUMENT = 4,
    }
}
