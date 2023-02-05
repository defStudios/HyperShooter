namespace Core.Services
{
    public class ServiceManager
    {
        public static ServiceManager Container => _instance ??= new ServiceManager();
        private static ServiceManager _instance;

        public void RegisterSingle<TService>(TService service) where TService : ISingleService =>
            Service<TService>.Instance = service;

        public TService Single<TService>() where TService : ISingleService =>
            Service<TService>.Instance;

        private static class Service<TService> where TService : ISingleService
        {
            public static TService Instance { get; set; }
        }
    }
}
