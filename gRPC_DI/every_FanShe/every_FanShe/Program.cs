using System;
using System.Reflection;

namespace every_FanShe
{
    public static class Demo
    {
        public static void MapGrpcService<TService>() where TService : class
        {
            Console.WriteLine(typeof(TService));
        }
        public static void Hello<T>(string name)
        {
            Console.WriteLine(typeof(T));
            Console.WriteLine(name);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
             Assembly assembly = Assembly.GetExecutingAssembly();
            Type type = assembly.GetType("every_FanShe.Demo");
            type.GetMethod("Hello").MakeGenericMethod(typeof(Demo)).Invoke(null, new[] { "wocao" });
            typeof(Demo).GetMethod("Hello").MakeGenericMethod(typeof(Demo)).Invoke(null,new[] { "wocao"});
            Console.Read();
        }
    }
}
