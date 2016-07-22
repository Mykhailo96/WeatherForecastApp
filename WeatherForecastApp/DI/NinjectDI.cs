using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Ninject.Web.Common;
using WeatherForecastApp.Models;
using WeatherForecastApp.Services;

namespace WeatherForecastApp.DI
{
    public class NinjectDI : IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectDI(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<IWebApi>().To<WeatherApi>();
            _kernel.Bind<IHomeService>().To<HomeService>();
        }
    }
}