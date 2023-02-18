public struct Token
{
    public Token()
    {
        this.Value = ' ';
        this.Kind = TokenKind.END_PROGRAM;
    }

    public Token(char value, TokenKind kind)
    {
        this.Value = value;
        this.Kind = kind;
    }

    public char Value { get; }
    public TokenKind Kind { get; }
}