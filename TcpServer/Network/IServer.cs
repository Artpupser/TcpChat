namespace TcpServer.Network;

public interface IServer
{
    public Task Start();
    public Task Stop();
}
