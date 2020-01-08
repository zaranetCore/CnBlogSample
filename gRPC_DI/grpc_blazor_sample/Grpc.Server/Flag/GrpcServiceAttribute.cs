using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grpc.Server.Flag
{
    public class GrpcServiceAttribute : Attribute
    {
        public bool IsStart { get; set; }
    }
}
