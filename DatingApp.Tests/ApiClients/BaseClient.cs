using System;
using System.Collections.Generic;
using System.Text;

namespace DatingApp.Tests.Clients
{
    public class BaseClient
    {
        const string baseUrl = "http://localhost";
        const int basePort = 5000;

        protected ApiCallsClient apiCallsClient = new ApiCallsClient(baseUrl, basePort);
    }
}
