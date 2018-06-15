using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command
{
    public class CreateTrafficViolationCommandExecutor : ICommandExecutor<CreateTrafficViolationCommand>
    {
        protected readonly IUnitOfWork UnitOfWork;

        public CreateTrafficViolationCommandExecutor(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(CreateTrafficViolationCommand command)
        {
            var newModel = UnitOfWork.BuildAggregate<TrafficViolation>();

            command.CopyValueTo(newModel);

            newModel.Add();

            await Task.CompletedTask;
        }
    }
}
