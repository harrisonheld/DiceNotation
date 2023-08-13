using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DiceNotation
{
    public class Tokenizer
    {
        public static List<Token> Tokenize(string input)
        {
            List<Token> tokens = new List<Token>();

            string pattern = @"\d+d\d+(keeplowest\d+|keephighest\d+)?|[\+\-*/]";
            MatchCollection matches = Regex.Matches(input, pattern);

            foreach (Match match in matches)
            {
                string tokenValue = match.Value;
                TokenType tokenType;

                if (Regex.IsMatch(tokenValue, @"\d+d\d+(keeplowest\d+|keephighest\d+)?"))
                {
                    tokenType = TokenType.DICE_ROLL;
                }
                else if (Regex.IsMatch(tokenValue, @"[\+\-*/]"))
                {
                    tokenType = TokenType.OPERATOR;
                }
                else
                {
                    throw new ArgumentException($"Unrecognized token: {tokenValue}");
                }

                tokens.Add(new Token(tokenType, tokenValue));
            }

            return tokens;
        }
    }
}