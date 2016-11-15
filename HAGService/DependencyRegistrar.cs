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
            builder.RegisterType<HAG.Service.Customer.CustomerService>().As<HAG.Interface.ICustomerService>();
            builder.RegisterType<HAG.Service.Assistance.AssistanceService>().As<HAG.Interface.IAssistanceService>();
            builder.RegisterType<HAG.Service.Mission.MissionService>().As<HAG.Interface.IMissionService>();
            builder.RegisterType<HAG.Service.MsgReqeust.MsgReqeustService>().As<HAG.Interface.IMsgReqeustService>();
            builder.RegisterType<HAG.Service.Profile.ProfileService>().As<HAG.Interface.IProfileService>();
            builder.RegisterType<HAG.Service.Search.SearchService>().As<HAG.Interface.ISearchService>();
            builder.RegisterType<HAG.Service.Shop.ShopService>().As<HAG.Interface.IShopService>();

            //builder.Register(c => new HAG.Service.Assistance.AssistanceBusiness()).As<HAG.Interface.IAssistanceService>().InstancePerRequest();


            // 4. Create and assign a dependency resolver for Web API to use.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }


        // Autofac 註冊範例

        ////  1. 用Name来区分不同的实现，代替As方法 ////
        //  containerBuilder.RegisterType<DbRepository>().Named<IRepository>("DB");
        //  containerBuilder.RegisterType<TestRepository>().Named<IRepository>("Test");
            
        //  IContainer container = containerBuilder.Build();
        //  var dbRepository = container.ResolveNamed<IRepository>("DB");
        //  var testRepository = container.ResolveNamed<IRepository>("Test");
    }
}