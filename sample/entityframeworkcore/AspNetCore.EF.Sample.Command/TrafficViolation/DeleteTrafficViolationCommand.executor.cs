using System.Threading.Tasks;
using AspNetCore.EF.Sample.Core.TrafficViolation;
using Daybreaksoft.Pattern.CQRS;
using Daybreaksoft.Pattern.CQRS.Command;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Command.TrafficViolation
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
            //var model = AggregateBus.BuildAggregate<TrafficViolationModel>(command.TrafficViolationId);

            //await model.RemoveAsync();
        }
    }
}
