using System.Threading.Tasks;
using AspNetCore.EF.Sample.Data.Entities;
using Daybreaksoft.Pattern.CQRS.Command;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Command.Vehicle
{
    public class DeleteVehicleCommandExecutor : ICommandExecutor<DeleteVehicleCommand>
    {
        protected readonly IDomainAppServiceFactory DomainAppServiceFactory;

        public DeleteVehicleCommandExecutor(IDomainAppServiceFactory domainAppServiceFactory)
        {
            DomainAppServiceFactory = domainAppServiceFactory;
        }

        public async Task ExecuteAsync(DeleteVehicleCommand command)
        {
            var vehicleAppService = DomainAppServiceFactory.GetDomainAppService<VehicleEntity>();

            await vehicleAppService.DeleteAsync(command.VehicleId);
        }
    }
}
