using Unity;
using Unity.Lifetime;

namespace pz_18Request.Services
{
    public static class RepoContainer
    {
        private static IUnityContainer _container;

        static RepoContainer()
        {
            _container = new UnityContainer();
 
            _container.RegisterType<IRequestRepository, RequestRepository>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ICommentRepository, CommentRepository>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IClientRepository, ClientRepository>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IRequestStatusRepository, RequestStatusRepository>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IDeviceModelRepository, DeviceModelRepository>(new ContainerControlledLifetimeManager());
        }

        public static IUnityContainer Container
        {
            get { return _container; }
        }
    }
}
