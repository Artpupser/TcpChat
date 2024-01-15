namespace TcpServer.Other;

public class Terminal
{
    public static void Log(string msg, ConsoleColor color = ConsoleColor.Gray)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(msg);
        Console.ForegroundColor = ConsoleColor.Gray;
    }
}
