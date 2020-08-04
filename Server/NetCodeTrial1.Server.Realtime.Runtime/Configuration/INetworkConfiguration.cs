using System.Net;
using System.Threading.Tasks;

namespace NetCodeTrial1.Server.Realtime.Runtime.Configuration
{
    public interface INetworkConfiguration
    {
        Task<IPEndPoint> GetPublicIPAddress();
    }
}
