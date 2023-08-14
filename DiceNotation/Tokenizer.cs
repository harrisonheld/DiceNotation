using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DiceNotation
{
    public class Tokenizer
    {
        private string _input;

        public Tokenizer(string input)
        {
            _input = input;
            
            _input = Preprocess(_input);
        }

        private string Preprocess(string before)
        {
            // Convert to lowercase
            string lowercaseString = before.ToLower();
            // Remove all whitespace characters
            string processedString = string.Join("", lowercaseString.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));

            return processedString;
        }

        public List<Token> Tokenize()
        {
            List<(string pattern, TokenType type)> tokenPatterns = new List<(string, TokenType)>
            {
                (@"[0-9]+", TokenType.NUMBER),           // Match integers
                (@"[-+*/]", TokenType.BINARY_OPERATOR),  // Match binary operators -, +, *, /
                (@"d", TokenType.ROLL),                  // Match d
                (@"[kd][lh]", TokenType.ROLL_MODIFIER),  // Match kh (keep highest), kl (keep lowest), dh (drop highest), dl (drop lowest)
                (@"\!", TokenType.ROLL_MODIFIER),        // Match ! (exploding dice)
                (@"\(", TokenType.LPAREN),               // Match left parenthesis
                (@"\)", TokenType.RPAREN),               // Match right parenthesis
            };

            List<Token> tokens = new List<Token>();
            int index = 0;

            while (index < _input.Length)
            {
                string substr = _input.Substring(index);

                bool couldMatch = false;
                foreach ((string pattern, TokenType type) in tokenPatterns)
                {
                    string patternFromBeginning = $"^{pattern}";

                    Match match = Regex.Match(substr, patternFromBeginning);
                    if (match.Success)
                    {
                        tokens.Add(new Token(type, match.Value));
                        index += match.Value.Length;
                        couldMatch = true;
                        break;
                    }
                }

                if(!couldMatch)
                {
                    throw new Exception($"Invalid token at index {index}: '{substr}'");
                }
            }

            return tokens;
        }
    }
}