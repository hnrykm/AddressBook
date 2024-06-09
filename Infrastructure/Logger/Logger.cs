using Microsoft.Extensions.Options;

namespace Backend.Interview.Api.Infrastructure.Logger;

public class Logger
{
    private readonly AppSettings _appSettings;

    public Logger(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public void WriteLog(string message)
    {
        string logPath = _appSettings.LogPath;

        using (StreamWriter writer = new StreamWriter(logPath, true))
        {
            writer.WriteLine($"{DateTime.Now} : {message}");
        }
    }
}
