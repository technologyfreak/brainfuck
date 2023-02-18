public class Lexer
{
    StreamReader reader;
    Token[] tokenLiterals;

    public Lexer(Stream content)
    {
        this.reader = new StreamReader(content);
        this.tokenLiterals = new Token[] {
                new Token('>', TokenKind.STEP_RIGHT),
                new Token('<', TokenKind.STEP_LEFT),
                new Token('+', TokenKind.INCREMENT),
                new Token('-', TokenKind.DECREMENT),
                new Token(',', TokenKind.INPUT),
                new Token('.', TokenKind.OUTPUT),
                new Token('[', TokenKind.BEGIN_LOOP),
                new Token(']', TokenKind.END_LOOP),
            };
    }

    ~Lexer()
    {
        this.reader.Close();
    }

    void Trim()
    {
        while (!reader.EndOfStream && Char.IsWhiteSpace((char)reader.Peek())) reader.Read();
    }

    public Token Next()
    {
        Trim();
        Token token = new Token();

        if (reader.EndOfStream)
        {
            reader.Close();
            return token;
        }

        foreach (Token literal in tokenLiterals)
        {
            if ((char)reader.Peek() == literal.Value)
            {
                token = literal;
                reader.Read();
                return token;
            }
        }

        token = new Token((char)reader.Read(), TokenKind.COMMENT);
        return token;
    }
}