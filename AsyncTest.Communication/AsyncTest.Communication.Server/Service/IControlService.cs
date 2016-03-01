using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTest.Communication.Server.Service
{
    public interface IControlService
    {
        bool ShouldPoll { get; set; }
    }
}
