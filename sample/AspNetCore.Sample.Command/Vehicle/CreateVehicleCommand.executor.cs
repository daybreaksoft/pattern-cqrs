using System.Threading.Tasks;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command.Vehicle
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
            var newModel = UnitOfWork.BuildAggregate<Domain.Models.Vehicle>();
            command.CopyValueTo(newModel);

            await Task.CompletedTask;
        }
    }
}
