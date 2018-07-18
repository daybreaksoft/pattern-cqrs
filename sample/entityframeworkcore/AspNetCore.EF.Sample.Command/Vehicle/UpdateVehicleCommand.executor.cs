using System.Threading.Tasks;
using AspNetCore.EF.Sample.Data.Entities;
using Daybreaksoft.Pattern.CQRS.Command;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Command.Vehicle
{
    public class UpdateVehicleCommandExecutor : ICommandExecutor<UpdateVehicleCommand>
    {
        protected readonly IDomainServiceFactory DomainAppServiceFactory;

        public UpdateVehicleCommandExecutor(IDomainServiceFactory domainAppServiceFactory)
        {
            DomainAppServiceFactory = domainAppServiceFactory;
        }

        public async Task ExecuteAsync(UpdateVehicleCommand command)
        {
            var vehicleAppService = DomainAppServiceFactory.GetDomainService<VehicleEntity>();

            var vehicleModel = await vehicleAppService.FindAsync(command.VehicleId);
            vehicleModel.UserId = command.UserId;
            vehicleModel.PlateNumber = command.PlateNumber;

            await vehicleAppService.UpdateAsync(vehicleModel);
        }
    }
}
