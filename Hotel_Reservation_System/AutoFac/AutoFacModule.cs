
using Autofac;
using Module = Autofac.Module;

namespace Hotel_Reservation_System.AutoFac;
public class AutoFacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
        builder.RegisterAssemblyTypes(typeof(IRoomMediator).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
        builder.RegisterAssemblyTypes(typeof(IFacilityMediator).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
        builder.RegisterAssemblyTypes(typeof(IRoomService).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
        builder.RegisterAssemblyTypes(typeof(IFacilitiesService).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
    }
}
