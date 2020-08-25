using System.Threading;
using System.Threading.Tasks;
using Refit;

namespace Grach.Core.Interfaces
{
    [Headers("Content-Type: application/json;encoding=UTF-8")]
    public interface ITestApi
    {
        [Get("/showuser")]
        Task<string> Authorize(CancellationToken parameters);
    }
}