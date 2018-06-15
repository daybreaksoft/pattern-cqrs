using System.Threading.Tasks;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command.Vehicle
{
    public class DeleteVehicleCommandExecutor : ICommandExecutor<DeleteVehicleCommand>
    {
        protected readonly IUnitOfWork UnitOfWork;

        public DeleteVehicleCommandExecutor(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(DeleteVehicleCommand command)
        {
            var model = UnitOfWork.BuildAggregate<Domain.Models.Vehicle>();
            model.VehicleId = command.VehicleId;

            model.Remove();

            await Task.CompletedTask;
        }
    }
}
