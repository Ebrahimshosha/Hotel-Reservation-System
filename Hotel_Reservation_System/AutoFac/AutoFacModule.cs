
using Autofac;
using Hotel_Reservation_System.Mediators.ReservationMediator;
using Hotel_Reservation_System.Services.ReservationService;
using Module = Autofac.Module;

namespace Hotel_Reservation_System.AutoFac;
public class AutoFacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(typeof(IRoomRepository).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
       
        builder.RegisterAssemblyTypes(typeof(IRoomMediator).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
        builder.RegisterAssemblyTypes(typeof(IFacilityMediator).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
		builder.RegisterAssemblyTypes(typeof(IReservationMediator).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
      
        builder.RegisterAssemblyTypes(typeof(IRoomService).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
        builder.RegisterAssemblyTypes(typeof(IFacilitiesService).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
        builder.RegisterAssemblyTypes(typeof(IReservationService).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();

	}
}
