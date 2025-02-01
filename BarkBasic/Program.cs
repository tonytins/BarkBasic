using BarkBasic;

class Program
{
    static void Main()
    {
        var interpreter = new BasicInterpreter();
        interpreter.RegisterCommand("PRINT", PrintCommand);
        interpreter.RegisterCommand("LET", LetCommand);
        // Add more commands as needed

        string basicCode = @"
            PRINT ""Hello, World!""
            LET A = 10
            PRINT A
        ";

        interpreter.Run(basicCode);
    }

    static void PrintCommand(List<string> args)
    {
        Console.WriteLine(string.Join(" ", args));
    }

    static void LetCommand(List<string> args)
    {
        if (args.Count != 2 || !int.TryParse(args[1], out int value))
        {
            Console.WriteLine("LET command syntax error");
            return;
        }

        // Store the variable and its value in a dictionary or context
        Console.WriteLine($"Variable {args[0]} set to {value}");
    }
}