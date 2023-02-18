public static class Program
{
    public static void Main(string[] args)
    {
        using (FileStream stream = File.OpenRead(args[0]))
        {

            Lexer lexer = new Lexer(stream);
            Token token = new Token();
            token = lexer.Next();
            List<TokenKind> commands = new List<TokenKind>();

            while (token.Kind != TokenKind.END_PROGRAM)
            {
                commands.Add(token.Kind);
                token = lexer.Next();
            }

            char[] program = new char[30_000];
            int programCursor = 0;
            int currentCommand = 0;
            Stack<int[]> jumps = new Stack<int[]>();

            for (; currentCommand < commands.Count; currentCommand++)
            {
                switch (commands[currentCommand])
                {
                    case TokenKind.STEP_RIGHT:
                        programCursor++;
                        break;
                    case TokenKind.STEP_LEFT:
                        programCursor--;
                        break;
                    case TokenKind.INCREMENT:
                        program[programCursor]++;
                        break;
                    case TokenKind.DECREMENT:
                        program[programCursor]--;
                        break;
                    case TokenKind.INPUT:
                        program[programCursor] = (char)Console.Read();
                        break;
                    case TokenKind.OUTPUT:
                        Console.Write(program[programCursor]);
                        break;
                    case TokenKind.BEGIN_LOOP:
                        /*
                            programCursor = counter index
                            currentCommand = loop starting index
                        */
                        jumps.Push(new int[] { programCursor, currentCommand });
                        break;
                    case TokenKind.END_LOOP:
                        // if counter value for our loop is zero, jump to beginning of loop
                        if (program[jumps.Peek()[0]] != 0) currentCommand = jumps.Peek()[1];
                        else
                        {
                            jumps.Pop();
                            continue;
                        }
                        break;
                    default:
                        break;
                }
            }

        }
    }
}