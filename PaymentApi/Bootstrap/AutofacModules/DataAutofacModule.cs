using Autofac;
using PaymentApi.Core;
using PaymentApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Bootstrap.AutofacModules
{
    public class DataAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterAssemblyTypes(GetType().Assembly)
                .Where(t => t.GetInterfaces().Any(i => i.Name.EndsWith("Repository")))
                .As(t => t.GetInterfaces().Where(i => i.Name.EndsWith("Repository")))
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>()
                .InstancePerLifetimeScope();

        }
    }
}
