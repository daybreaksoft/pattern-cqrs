using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using AspNetCore.Sample.Domain.Models.UserDomain;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;
using Daybreaksoft.Pattern.CQRS.Command;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.Sample.Command
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
            var vehicle = new Vehicle(command.UserId, command.PlateNumber);

            var vehicleAppService = DomainAppServiceFactory.GetDomainAppService<Vehicle>();

            await vehicleAppService.InsertAsync(vehicle);
        }
    }
}
