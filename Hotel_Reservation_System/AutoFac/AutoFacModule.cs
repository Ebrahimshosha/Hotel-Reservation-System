
using Autofac;
using Microsoft.AspNetCore.Cors.Infrastructure;

using Module = Autofac.Module;

namespace Hotel_Reservation_System.AutoFac;
public class AutoFacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        //builder.RegisterType<Context>().InstancePerLifetimeScope();
    }

}
