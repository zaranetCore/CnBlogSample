using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Grpc.Server.Common
{
    public static class ServicesHelper
    {
        public static Dictionary<string,string> GetGrpcServices(string assemblyName)
        {
            if (!string.IsNullOrEmpty(assemblyName))
            {
                Assembly assembly = Assembly.Load(assemblyName);
                List<Type> ts = assembly.GetTypes().ToList();

                var result = new Dictionary<string, string>();
                foreach (var item in ts.Where(u=>u.CustomAttributes.Any(a=>a.AttributeType.Name == "GrpcServiceAttribute")))
                {
                    result.Add(item.Name,item.Namespace);
                }
                return result;
            }
            return new Dictionary<string, string>();
        }
        /// <summary>  
        /// 获取程序集中的实现类对应的多个接口
        /// </summary>  
        /// <param name="assemblyName">程序集</param>
        public static Dictionary<Type, Type[]> GetClassName(string assemblyName)
        {
            if (!String.IsNullOrEmpty(assemblyName))
            {
                Assembly assembly = Assembly.Load(assemblyName);
                List<Type> ts = assembly.GetTypes().ToList();

                var result = new Dictionary<Type, Type[]>();
                foreach (var item in ts.Where(s => !s.IsInterface))
                {
                    var interfaceType = item.GetInterfaces();
                    result.Add(item, interfaceType);
                }
                return result;
            }
            return new Dictionary<Type, Type[]>();
        }
    }
}
