namespace BarkBasic;

internal class Parser
{
    readonly Lexer _lexer;
    Token _currentToken;

    public Parser(Lexer lexer)
    {
        _lexer = lexer;
        _currentToken = _lexer.GetNextToken();
    }

    void Eat(TokenType tokenType)
    {
        if (_currentToken.Type == tokenType)
            _currentToken = _lexer.GetNextToken();
        else
            throw new Exception("Error parsing input");
    }

    public int Parse()
    {
        var result = Term();

        while (_currentToken.Type is TokenType.Plus or TokenType.Minus)
        {
            switch (_currentToken.Type)
            {
                case TokenType.Plus:
                    Eat(TokenType.Plus);
                    result += Term();
                    break;
                case TokenType.Minus:
                    Eat(TokenType.Minus);
                    result -= Term();
                    break;
                case TokenType.Print:
                    Eat(TokenType.Print);
                    break;
            }
        }

        return result;
    }

    int Term()
    {
        var result = Factor();

        while (_currentToken.Type is TokenType.Multiply or TokenType.Divide)
        {
            switch (_currentToken.Type)
            {
                case TokenType.Multiply:
                    Eat(TokenType.Multiply);
                    result *= Factor();
                    break;
                case TokenType.Divide:
                    Eat(TokenType.Divide);
                    result /= Factor();
                    break;
            }
        }

        return result;
    }

    int Factor()
    {
        var token = _currentToken;

        switch (token.Type)
        {
            case TokenType.Number:
                Eat(TokenType.Number);
                return int.Parse(token.Value ?? throw new InvalidOperationException());
            case TokenType.LParen:
            {
                Eat(TokenType.LParen);
                var result = Parse();
                Eat(TokenType.RParen);
                return result;
            }
            case TokenType.Plus:
            case TokenType.Minus:
            case TokenType.Multiply:
            case TokenType.Divide:
            case TokenType.RParen:
            case TokenType.Print:
            case TokenType.EOF:
            default:
                throw new Exception("Error parsing input");
        }
    }
}