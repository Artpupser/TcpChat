using Newtonsoft.Json;

namespace TcpLib.IO;

public interface IJsonObject
{
    public Task<string> Serialize()
    {
        return Task.FromResult(JsonConvert.SerializeObject(this));
    }
}
