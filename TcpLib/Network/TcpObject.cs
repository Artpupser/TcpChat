using System.Text;
using TcpLib.IO;

namespace TcpLib.Network;

public class TcpObject : IJsonObject
{
    public static Encoding Encoding = Encoding.UTF8;
    private string _request;
    private object _form;

    public TcpObject(string request, object form)
    {
        Request = request;
        Form = form;
    }

    public string Request { get => _request; set => _request = value; }
    public object Form { get => _form; set => _form = value; }
}