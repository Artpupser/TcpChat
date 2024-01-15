using Newtonsoft.Json;
using System.Diagnostics;
using TcpLib.IO;
using TcpLib.Logs;
using TcpServer.Network;
using TcpServer.Other;

namespace TcpServer;

public class Program
{
    public static async Task Main(string[] args)
    {
        await new Program().Startup();
    }
    public async Task Startup()
    {
        Console.Clear();
        CatchManager.ExcpetionFinded += CatchLog;
        AppDomain.CurrentDomain.UnhandledException += CatchManager.UnhandledExceptionHandler;
        go:
        if (File.Exists(ServerConfig.Path))
        {
            var serverConfig = FileManager.GetJson<ServerConfig>(ServerConfig.Path).Result;
            var server = new Server(serverConfig);
            await server.Start();
            if (serverConfig.Host == "" || serverConfig.Port == 00000)
                await SettingMessage();
        }
        else
        {
            IJsonObject jsonObj = ServerConfig.Empty;
            await FileManager.SetFile(ServerConfig.Path, jsonObj.Serialize().Result);
            await SettingMessage();
            goto go;
        }
    }
    public async Task SettingMessage()
    {
        await Console.Out.WriteLineAsync("Setting serverConfig.json");
        await Console.Out.WriteLineAsync(ServerConfig.Path);
        var info = new ProcessStartInfo
        {
            UseShellExecute = false,
            FileName = "notepad.exe",
            Arguments = ServerConfig.Path
        };
        Process.Start(info);
        await Console.Out.WriteLineAsync("Press [Enter] if ready!");
        Console.ReadKey(true);
    }
    public async Task CatchLog(string content)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        await Console.Out.WriteLineAsync(content);
        Console.ForegroundColor = ConsoleColor.Gray;
    }

}