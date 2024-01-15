using System.Text;

namespace TcpLib.Logs;

public class CatchManager
{
    public delegate Task Throw(string content);
    public static event Throw ExcpetionFinded;
    public static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
    {
        var exception = args.ExceptionObject as Exception;
        ExcpetionFinded?.Invoke(GetLogs(exception).Result);
    }
    public static Task Catch(Action action)
    {
        try
        {
            action.Invoke();
        }
        catch (Exception e)
        {
            ExcpetionFinded?.Invoke(GetLogs(e).Result);
        }
        return Task.CompletedTask;
    }
    public static Task<string> GetLogs(Exception e)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Message: {e.Message}");
        sb.AppendLine($"Source: {e.Source}");
        sb.AppendLine($"InnerException: {e.InnerException}");
        sb.AppendLine($"StackTrace: {e.StackTrace}");
        sb.AppendLine($"Data: {e.Data}");
        sb.AppendLine($"HelpLink: {e.HelpLink}");
        sb.AppendLine($"InnerException: {e.InnerException}");
        sb.AppendLine($"TargetSite: {e.TargetSite}");
        sb.AppendLine($"HResult: {e.HResult}");
        sb.Append($"Date: {DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss")}");
        return Task.FromResult(sb.ToString());
    }
}
