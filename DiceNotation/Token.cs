using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceNotation
{
    public enum TokenType
    {
        NUMBER,
        BINARY_OPERATOR, // +, -, *, /
        ROLL, // d
        ROLL_MODIFIER, // modifes a d operator, 'keephighest', 'keeplowest', 'drophighest', 'droplowest', '!'
        LPAREN,
        RPAREN
    }

    public class Token
    {
        public TokenType Type { get; }
        public string Value { get; }

        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}
