using System.Net;
using System.Net.Sockets;
using TcpLib.Logs;
using static TcpServer.Other.Terminal;

namespace TcpServer.Network;

public class Server : IServer
{
    private ServerConfig _config;
    private bool _stop = false;
    private List<TcpClientObject> _clients = new();
    private TcpListener _server { get; set; }
    public Server(ServerConfig config)
    {
        _config = config;
    }
    public async Task FakeConnect()
    {
        var ipEndPoint = await _config.GetIPEndPoint();
        var client = new TcpClient();
        await client.ConnectAsync(ipEndPoint);
    }
    public async Task Start()
    {
        var ipEndPoint = await _config.GetIPEndPoint();
        var tasks = new List<Task>();
        _server = new TcpListener(ipEndPoint);
        _server.Start();
        Log("Server started", ConsoleColor.Green);
        Log(_config.ToString(), ConsoleColor.Yellow);
        //Todo Регистрация, Загрузка клиента, Переписка + Connect DB [Sql or Sqlite]
        for (int i = 1; i <= 2; i++)
        {
            _ = Task.Run(FakeConnect);
        }
        while (!_stop)
        {
            var client = await _server.AcceptTcpClientAsync();
            var clientObj = new TcpClientObject(this, client);
            if (_clients.CheckIp(clientObj).Result)
            {
                Log($"Client connected! {clientObj.Ip.Address}:{clientObj.Ip.Port}", ConsoleColor.Green);
                _clients.Add(clientObj);
                tasks.Add(Task.Run(() => { clientObj.ClientProcessor(); }));
            }
        }
    }
    public Task Stop()
    {
        return Task.CompletedTask;
    }
}
public static class TcpClientObjectExtensions
{
    public static Task<bool> CheckIp(this List<TcpClientObject> list, TcpClientObject client)
    {
        var i = list.FindIndex(i => i.Ip.Address.ToString() == client.Ip.Address.ToString());
        return Task.FromResult(i == -1);

    }
}
public class TcpClientObject
{
    private TcpClient _client;
    private IPEndPoint _ip;
    private NetworkStream _stream;
    private object _server;

    public TcpClientObject(Server server, TcpClient client)
    {
        Client = client;
        Stream = client.GetStream();
        Server = server;
        Ip = (IPEndPoint)client.Client.RemoteEndPoint;
    }
    public Task ClientProcessor()
    {
        CatchManager.Catch(async () =>
        {
            while (true)
            {
                await Task.Delay(1000);
                Log("Добрый день коллеги :)");
            }
        });
        return Task.CompletedTask;
    }

    public TcpClient Client { get => _client; set => _client = value; }
    public NetworkStream Stream { get => _stream; set => _stream = value; }
    public object Server { get => _server; set => _server = value; }
    public IPEndPoint Ip { get => _ip; set => _ip = value; }
}