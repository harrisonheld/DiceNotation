using DiceNotation;

namespace DiceNotationTests
{
    public class TokenizerTests
    {
        [Fact]
        public void Test1()
        {
            string input = "3d6keeplowest2 + 1d6 + 2";

            List<Token> expected = new List<Token>()
            {
                new Token(TokenType.DICE_ROLL, "3d6keeplowest2"),
                new Token(TokenType.OPERATOR, "+"),
                new Token(TokenType.DICE_ROLL, "1d6"),
                new Token(TokenType.OPERATOR, "+"),
                new Token(TokenType.CONSTANT, "2")
            };

            List<Token> actual = Tokenizer.Tokenize(input);

            Assert.Equal(expected, actual, new TokenComparer());
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