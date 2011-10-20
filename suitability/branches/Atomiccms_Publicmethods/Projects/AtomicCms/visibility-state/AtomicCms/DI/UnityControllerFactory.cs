namespace AtomicCms.Web.Core.DI
{
    using System;
    using System.Configuration;
     using System.Web.Mvc;
    using Common.CodeContract;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Configuration;

    public class UnityControllerFactory : DefaultControllerFactory
    {
        private readonly IUnityContainer container;

        public UnityControllerFactory()
        {
            container = new UnityContainer();
            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            section.Containers["containerOne"].Configure(container);
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            Check.Argument.IsNotNull(controllerType, "controllerType");

            return container.Resolve(controllerType) as IController;
            //return base.GetControllerInstance(requestContext, controllerType);
        } 
    }
}