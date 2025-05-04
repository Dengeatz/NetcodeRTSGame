using System;
using System.Collections.Generic;

namespace RTS.Assets.Game._Scripts.Game
{
    public static class ServiceLocator 
    {
        private readonly static Dictionary<Type, object> _services = new();

        public static T GetService<T>()
        {
            return (T)_services[typeof(T)];
        }

        public static void Register<T>(T Service)
        {
            _services[typeof(T)] = Service;
        }

        public static void Unregister<T>()
        {
            _services.Remove(typeof(T));
        }

        public static void Reset()
        {
            _services.Clear();
        }
    }
}
