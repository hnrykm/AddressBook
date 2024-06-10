using Backend.Interview.Api.ApplicationCore.Contracts;
using Microsoft.Extensions.Options;

namespace Backend.Interview.Api.Infrastructure.Logger;

public class CustomLogger : ICustomLogger
{
    private readonly AppSettings _appSettings;

    public CustomLogger(IOptions<AppSettings> appSettings)
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
