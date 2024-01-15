namespace TcpLib.Network;
[AttributeUsage(AttributeTargets.Method)]
public class TcpRequestAttribute : Attribute
{
    public readonly string tcpRequest;
    public TcpRequestAttribute(string tcpRequest)
    {
        this.tcpRequest = tcpRequest;
        throw new NotImplementedException();
    }
}
