using BarkBasic;
using Tomlyn;

var projModel = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Project.toml"));
var projFile = Toml.ToModel<ProjectFile>(projModel);
var code = "";

try
{
    var path = Path.Combine(Environment.CurrentDirectory, projFile.Code);
    if (File.Exists(path))
        code = File.ReadAllText(path);
}
catch (Exception err)
{
    throw new FileNotFoundException(err.StackTrace);
}

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
    
    // TODO: Finish goto function.
    var gotoMatch = gotoRegex.Match(line);
    if (gotoMatch.Success)
    {
        /*using var reader = new StreamReader(projFile.Code);

         for (var i = 1; i < int.Parse(gotoMatch.Groups[1].Value); i++)
             reader.ReadLine();
         
         Console.WriteLine(reader.ReadLine());*/
    }

    throw new Exception($"Unknown statement in line: {line}");
}
