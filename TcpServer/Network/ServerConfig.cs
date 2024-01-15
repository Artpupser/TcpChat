using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TcpLib.IO;

namespace TcpServer.Network;

public record class ServerConfig : IJsonObject
{
    public static string Path = $"{FileManager.DefaultPath}serverConfig.json";
    private string _host;
    private int _port;

    [JsonConstructor]
    public ServerConfig(string host, int port)
    {
        
        Host = host;
        Port = port;
    }
    public Task<IPEndPoint> GetIPEndPoint()
    {
        var ipAdress = IPAddress.Parse(Host);
        var ipEndPoint = new IPEndPoint(ipAdress, Port);
        return Task.FromResult(ipEndPoint);
    }
    public static ServerConfig Empty = new("Empty",00000);
    public string Host { get => _host; set => _host = value; }
    public int Port { get => _port; set => _port = value; }
}
