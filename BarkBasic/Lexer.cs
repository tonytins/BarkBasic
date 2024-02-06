namespace BarkBasic;

internal class Lexer(string input)
{
    static readonly Regex NumberRegex = new(@"\d+");
    static readonly Regex WhitespaceRegex = new(@"\s+");
    int _pos = 0;

    public Token GetNextToken()
    {
        while (true)
        {
            if (_pos >= input.Length)
                return new Token(TokenType.EOF, null);

            if (char.IsDigit(input[_pos]))
            {
                var match = NumberRegex.Match(input, _pos);
                _pos += match.Length;
                return new Token(TokenType.Number, match.Value);
            }

            if (!char.IsWhiteSpace(input[_pos]))
                return input[_pos++] switch
                {
                    '+' => new Token(TokenType.Plus, "+"),
                    '-' => new Token(TokenType.Minus, "-"),
                    '*' => new Token(TokenType.Multiply, "*"),
                    '/' => new Token(TokenType.Divide, "/"),
                    '(' => new Token(TokenType.LParen, "("),
                    ')' => new Token(TokenType.RParen, ")"),
                    _ => throw new Exception("Error parsing input")
                };
            
            _pos += WhitespaceRegex.Match(input, _pos).Length;
        }
    }
}