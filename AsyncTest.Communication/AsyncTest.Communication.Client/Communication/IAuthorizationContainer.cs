using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTest.Communication.Client.Communication
{
    public interface IAuthorizationContainer
    {
        string AppKey { get; set; }
        string SharedSecret { get; set; }
    }
}
