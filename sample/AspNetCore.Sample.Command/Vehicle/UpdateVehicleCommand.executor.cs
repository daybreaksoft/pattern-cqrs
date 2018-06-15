using System.Threading.Tasks;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command
{
    public class UpdateVehicleCommandExecutor : ICommandExecutor<UpdateVehicleCommand>
    {
        protected readonly IUnitOfWork UnitOfWork;

        public UpdateVehicleCommandExecutor(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(UpdateVehicleCommand command)
        {
            var model = await UnitOfWork.GetAggregate<Domain.Models.Vehicle>(command.VehicleId);

            model.Modify();

            command.CopyValueTo(model);
        }
    }
}
