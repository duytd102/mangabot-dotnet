using System;

namespace WebScraper.Utils
{
    class MethodInvoker<T>
    {
        public T Invoke(string fullClassName, string method, object[] args)
        {
            object instance = Activator.CreateInstance(Type.GetType(fullClassName));
            object returnedValue = instance.GetType().GetMethod(method).Invoke(instance, args);
            return (T)Convert.ChangeType(returnedValue, typeof(T));
        }
    }
}
