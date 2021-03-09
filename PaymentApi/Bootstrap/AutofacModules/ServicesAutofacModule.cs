using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Bootstrap.AutofacModules
{
    public class ServicesAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = GetType().Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.GetInterfaces().Any(i => i.Name.EndsWith("Service")))
                .As(t => t.GetInterfaces().Where(i => i.Name.EndsWith("Service")))
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.GetInterfaces().Any(i => i.Name.EndsWith("PaymentGateway")))
                .As(t => t.GetInterfaces().Where(i => i.Name.EndsWith("PaymentGateway")))
                .InstancePerLifetimeScope();


        }
    }
}
