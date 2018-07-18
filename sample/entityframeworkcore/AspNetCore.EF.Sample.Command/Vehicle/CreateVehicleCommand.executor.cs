using System.Threading.Tasks;
using AspNetCore.EF.Sample.Data.Entities;
using Daybreaksoft.Pattern.CQRS.Command;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Command.Vehicle
{
    public class CreateVehicleCommandExecutor : ICommandExecutor<CreateVehicleCommand>
    {
        protected readonly IUnitOfWork UnitOfWork;

        public CreateVehicleCommandExecutor(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(CreateVehicleCommand command)
        {
            var vehicle = new VehicleEntity(command.UserId, command.PlateNumber);

            UnitOfWork.ReadyToAdd(vehicle);

            await Task.CompletedTask;
        }
    }
}
