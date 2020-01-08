using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Server.Flag;
using Microsoft.Extensions.Logging;

namespace Grpc.Server.Services
{
    [GrpcService]
    public class GreeterService : Greeter.GreeterBase
    {
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            var greeting = string.Empty;
            switch (request.LaguageEnum)
            {
                case HelloRequest.Types.Laguage.EnUs:
                    greeting = "Hello";
                    break;
                case HelloRequest.Types.Laguage.ZhCn:
                    greeting = "ÄãºÃ";
                    break;
            }
            return Task.FromResult(new HelloReply
            {
                Message = $"{greeting} {request.Name}",
                Num = new Random().Next()
            });
        }
    }
}
