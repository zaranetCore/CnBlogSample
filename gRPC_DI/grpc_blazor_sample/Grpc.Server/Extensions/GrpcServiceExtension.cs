using Grpc.Server;
using Grpc.Server.Common;
using Grpc.Server.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Grpc.Server.Extensions
{
    public static class GrpcServiceExtension
    {
        public static void Add_Grpc_Services(IEndpointRouteBuilder builder)
        {
            Assembly assembly = Assembly.GetExecutingAssembly(); 
            foreach (var item in ServicesHelper.GetGrpcServices("Grpc.Server"))
            {
                Type mytype = assembly.GetType(item.Value + "."+item.Key);
                var method = typeof(GrpcEndpointRouteBuilderExtensions).GetMethod("MapGrpcService").MakeGenericMethod(mytype);
                method.Invoke(null, new[] { builder }); 
            };
        }
        public static void useMyGrpcServices(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                Add_Grpc_Services(endpoints);
            });
        }
    }
}
