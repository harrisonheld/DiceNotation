using DiceNotation;

namespace DiceNotationTests
{
    public class TokenizerTests
    {
        [Fact]
        public void TokenizerTest1()
        {
            string input = "10d6! + (2d4kh1 - 1)";
            List<Token> expected = new List<Token>
            {
                new Token(TokenType.NUMBER, "10"),
                new Token(TokenType.ROLL, "d"),
                new Token(TokenType.NUMBER, "6"),
                new Token(TokenType.ROLL_MODIFIER, "!"),
                new Token(TokenType.BINARY_OPERATOR, "+"),
                new Token(TokenType.LPAREN, "("),
                new Token(TokenType.NUMBER, "2"),
                new Token(TokenType.ROLL, "d"),
                new Token(TokenType.NUMBER, "4"),
                new Token(TokenType.ROLL_MODIFIER, "kh"),
                new Token(TokenType.NUMBER, "1"),
                new Token(TokenType.BINARY_OPERATOR, "-"),
                new Token(TokenType.NUMBER, "1"),
                new Token(TokenType.RPAREN, ")")
            };

            Tokenizer tokenizer = new Tokenizer(input);
            List<Token> actual = tokenizer.Tokenize();

            Assert.Equal(expected, actual, new TokenComparer());
        }

        [Fact]
        public void TokenizerTest2()
        {
            string input = "(3d6)*(4d8) + (1d2)d8";
            List<Token> expected = new List<Token>
            {
                new Token(TokenType.LPAREN, "("),
                new Token(TokenType.NUMBER, "3"),
                new Token(TokenType.ROLL, "d"),
                new Token(TokenType.NUMBER, "6"),
                new Token(TokenType.RPAREN, ")"),
                new Token(TokenType.BINARY_OPERATOR, "*"),
                new Token(TokenType.LPAREN, "("),
                new Token(TokenType.NUMBER, "4"),
                new Token(TokenType.ROLL, "d"),
                new Token(TokenType.NUMBER, "8"),
                new Token(TokenType.RPAREN, ")"),
                new Token(TokenType.BINARY_OPERATOR, "+"),
                new Token(TokenType.LPAREN, "("),
                new Token(TokenType.NUMBER, "1"),
                new Token(TokenType.ROLL, "d"),
                new Token(TokenType.NUMBER, "2"),
                new Token(TokenType.RPAREN, ")"),
                new Token(TokenType.ROLL, "d"),
                new Token(TokenType.NUMBER, "8")
            };

            Tokenizer tokenizer = new Tokenizer(input);
            List<Token> actual = tokenizer.Tokenize();

            Assert.Equal(expected, actual, new TokenComparer());
        }

        [Fact]
        public void TokenizerFailsJunkInput()
        {
            string input = "womp womp";
            Tokenizer tokenizer = new Tokenizer(input);
            Assert.Throws<Exception>(tokenizer.Tokenize);
        }

        public class TokenComparer : IEqualityComparer<Token>
        {
            public bool Equals(Token x, Token y)
            {
                return x.Type == y.Type && x.Value == y.Value;
            }

            public int GetHashCode(Token obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}