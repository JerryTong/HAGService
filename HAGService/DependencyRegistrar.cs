using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace HAGService
{
    /// <summary>
    /// Autofac IoC
    /// </summary>
    public class DependencyRegistrar
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            // 1. Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // 2. Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());


            // 3. Register service to be used by the controller
            builder.RegisterType<HAG.Service.Assistance.AssistanceService>().As<HAG.Interface.IAssistanceService>();
            builder.RegisterType<HAG.Service.Customer.CustomerService>().As<HAG.Interface.ICustomerService>();
            //builder.Register(c => new HAG.Service.Assistance.AssistanceBusiness()).As<HAG.Interface.IAssistanceService>().InstancePerRequest();


            // 4. Create and assign a dependency resolver for Web API to use.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}