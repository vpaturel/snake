

namespace SerenitySystem.services
{
    public static class Services
    {
        private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public static void AddService<T>(T service)
        {
            if (_services.ContainsKey(typeof(T)))
            {
                throw new InvalidOperationException($"Service of type {typeof(T)} already exists");
            }
            else
            {
             _services[typeof(T)] = service;
            }
        }
        public static T GetService<T>()
        {
            if (_services.ContainsKey(typeof(T)))
            {
                return (T)_services[typeof(T)];
            }
            else
            {
                throw new InvalidOperationException($"Service of type {typeof(T)} does not exist");
            }
        }

    }
}
