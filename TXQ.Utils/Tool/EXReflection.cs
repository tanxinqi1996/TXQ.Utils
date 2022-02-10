using System;
using System.Reflection;

namespace TXQ.Utils.Tool
{
    public class EXReflection
    {
        public static object RunFunction(string ClassName, string FunctionName, object[] Args = null)
        {


            Type type = Type.GetType(ClassName);      // 通过类名获取同名类
            object obj = System.Activator.CreateInstance(type);       // 创建实例

            MethodInfo method = type.GetMethod(FunctionName, new Type[] { });      // 获取方法信息
            object[] parameters = null;
            method.Invoke(obj, parameters);                           // 调用方法，参数为空

            // 注意获取重载方法，需要指定参数类型
            method = type.GetMethod(FunctionName, new Type[] { typeof(string) });      // 获取方法信息
            method = type.GetMethod(FunctionName, new Type[] { typeof(string), typeof(string) });      // 获取方法信息
            return method.Invoke(obj, parameters);     // 调用方法，有参数，有返回值

        }
    }
}
