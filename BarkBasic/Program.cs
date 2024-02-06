const string code = """

                    10 PRINT 'Hello, World!'
                    20 REM This is a comment
                    30 GOTO 10

                    """;

var lines = code.Split(Environment.NewLine);
var lineNumberRegex = new Regex(@"^\d+");
var commentRegex = new Regex(@"REM.*$");
var printRegex = new Regex(@"PRINT\s+'(.*)'");
var gotoRegex = new Regex(@"GOTO\s+(\d+)");

foreach (var line in lines)
{
    if (string.IsNullOrWhiteSpace(line)) continue;

    var lineNumberMatch = lineNumberRegex.Match(line);
    if (!lineNumberMatch.Success) throw new Exception($"Invalid line number in line: {line}");

    var commentMatch = commentRegex.Match(line);
    if (commentMatch.Success) continue;  // Ignore comments

    var printMatch = printRegex.Match(line);
    if (printMatch.Success)
    {
        Console.WriteLine(printMatch.Groups[1].Value);
        continue;
    }

    var gotoMatch = gotoRegex.Match(line);
    if (gotoMatch.Success)
    {
        // Handle GOTO statement
        continue;
    }

    throw new Exception($"Unknown statement in line: {line}");
}
