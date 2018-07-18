using System.Threading.Tasks;
using AspNetCore.EF.Sample.Data.Entities;
using Daybreaksoft.Pattern.CQRS.Command;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Command.Vehicle
{
    public class CreateVehicleCommandExecutor : ICommandExecutor<CreateVehicleCommand>
    {
        protected readonly IDomainAppServiceFactory DomainAppServiceFactory;

        public CreateVehicleCommandExecutor(IDomainAppServiceFactory domainAppServiceFactory)
        {
            DomainAppServiceFactory = domainAppServiceFactory;
        }

        public async Task ExecuteAsync(CreateVehicleCommand command)
        {
            var vehicle = new VehicleEntity(command.UserId, command.PlateNumber);

            var vehicleAppService = DomainAppServiceFactory.GetDomainAppService<VehicleEntity>();

            await vehicleAppService.InsertAsync(vehicle);
        }
    }
}
