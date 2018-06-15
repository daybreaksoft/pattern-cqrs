using System.Threading.Tasks;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command
{
    public class UpdateTrafficViolationCommandExecutor : ICommandExecutor<UpdateTrafficViolationCommand>
    {
        protected readonly IUnitOfWork UnitOfWork;

        public UpdateTrafficViolationCommandExecutor(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(UpdateTrafficViolationCommand command)
        {
            var model = await UnitOfWork.GetAggregate<Domain.Models.TrafficViolation>(command.TrafficViolationId);

            command.CopyValueTo(model);

            model.Modify();
        }
    }
}
