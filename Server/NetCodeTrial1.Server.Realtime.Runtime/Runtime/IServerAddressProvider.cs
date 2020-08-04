using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetCodeTrial1.Server.Realtime.Runtime.Runtime
{
    public interface IServerAddressProvider
    {
        Task<IPEndPoint> GetServerAddress();
    }
}
