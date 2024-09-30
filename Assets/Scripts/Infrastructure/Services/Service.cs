using System;
using System.Collections.Generic;

namespace Infrastructure.Services
{
    public static class Service
    {
        private static readonly Dictionary<System.Type, object> Database = new ();
        
        public static void FindIfNull<T>(ref T service) where T : UnityEngine.Object
        {
            if (service == null)
            {
                service = Get<T>();
            }
        }
        
        public static T Get<T>() where T : UnityEngine.Object
        {
            if (Database.TryGetValue(typeof(T), out object service))
            {
                return (T)service;
            }
            else
            {
                T serviceInstance = UnityEngine.Object.FindAnyObjectByType<T>();
                if (serviceInstance == null)
                    throw new Exception("Can't find an object with type " + typeof(T).Name);
                Database.Add(typeof(T), serviceInstance);
                return serviceInstance;
            }
        }
    }
}