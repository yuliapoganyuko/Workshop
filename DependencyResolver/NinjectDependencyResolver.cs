using Ninject;
using Service.Interface;
using Service.Interface.Entities;
using Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DependencyResolver
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IService<PortfolioItem>>().To<PortfolioItemsService>();
            kernel.Bind<IUserService>().To<UsersService>();
        }
    }
}
