using System.Threading.Tasks;
using AspNetCore.EF.Sample.Data.Entities;
using Daybreaksoft.Pattern.CQRS.Command;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Command.Vehicle
{
    public class DeleteVehicleCommandExecutor : ICommandExecutor<DeleteVehicleCommand>
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IDomainServiceFactory DomainServiceFactory;

        public DeleteVehicleCommandExecutor(IUnitOfWork unitOfWork, IDomainServiceFactory domainServiceFactory)
        {
            UnitOfWork = unitOfWork;
            DomainServiceFactory = domainServiceFactory;
        }

        public async Task ExecuteAsync(DeleteVehicleCommand command)
        {
            var vehicle = await DomainServiceFactory.GetDomainService<VehicleEntity>().FindAsync(command.VehicleId);

            UnitOfWork.ReadyToRemove(vehicle);
        }
    }
}
