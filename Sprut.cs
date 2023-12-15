using Sprut;

internal class Program
{
    public static void Main(string[] args)
    {
        var file = @"D:\CSharpProjects\Sprut\Sprut\main.st";
        var code = File.ReadAllText(file);
        var tokens = new Lexer(code).Tokenize();
        var statements = new Parser(tokens).Parse();

        foreach (var statement in statements) statement.Exec();
        foreach (var arg in args) Console.WriteLine(arg);
    }
}
