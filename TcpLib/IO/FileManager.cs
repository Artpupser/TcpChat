using Newtonsoft.Json;
using System.Reflection;

namespace TcpLib.IO;

public class FileManager
{
    public static string DefaultPath = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\\";
    public static Task<T> FromJson<T>(string json) where T : IJsonObject
    {
        return Task.FromResult(JsonConvert.DeserializeObject<T>(json));
    }
    public static Task<T> GetJson<T>(string path) where T : IJsonObject
    {
        return Task.FromResult(JsonConvert.DeserializeObject<T>(GetFile(path).Result));
    }
    public static Task SetFile(string path, string content)
    {
        File.WriteAllText(path, content);
        return Task.CompletedTask;
    }
    public static Task<string> GetFile(string path)
    {
        return Task.FromResult(File.ReadAllText(path));
    }
}
