using Backend.Interview.Api.Infrastructure.Logger;
using Microsoft.Extensions.Options;

namespace Backend.Interview.Api.ApplicationCore.Contracts;

public interface ICustomLogger
{
    void WriteLog(string message);
}
