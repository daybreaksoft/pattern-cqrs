using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command
{
    public class DeleteTrafficViolationCommandExecutor : ICommandExecutor<DeleteTrafficViolationCommand>
    {
        protected readonly IUnitOfWork UnitOfWork;

        public DeleteTrafficViolationCommandExecutor(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(DeleteTrafficViolationCommand command)
        {
            var model = UnitOfWork.BuildAggregate<TrafficViolation>();
            model.Id = command.TrafficViolationId;

            model.Remove();

            await Task.CompletedTask;
        }
    }
}
