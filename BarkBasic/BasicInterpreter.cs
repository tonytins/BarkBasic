namespace BarkBasic;

public class BasicInterpreter
{
    private readonly Dictionary<string, Command> _commands = new();
    public void RegisterCommand(string name, Action<List<string>> action) => _commands[name] = new Command(action);

    public void Run(string code)
    {
        var lines = code.Split('\n');
        foreach (var line in lines)
        {
            ExecuteLine(line);
        }
    }

    private void ExecuteLine(string line)
    {
        line = line.Trim();
        if (string.IsNullOrEmpty(line)) return;

        var parts = line.Split(' ');
        var commandName = parts[0].ToUpper();
        var args = new List<string>(parts);
        args.RemoveAt(0); // Remove the command name from arguments

        if (_commands.ContainsKey(commandName))
        {
            _commands[commandName].Execute(args);
        }
        else
        {
            Console.WriteLine($"Unknown command: {commandName}");
        }
    }
}

public class Command
{
    private readonly Action<List<string>> _action;

    public Command(Action<List<string>> action) => _action = action;

    public void Execute(List<string> args) => _action.Invoke(args);
}