using System.Threading.Tasks;
using AspNetCore.EF.Sample.Core.TrafficViolation;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;
using Daybreaksoft.Pattern.CQRS.Command;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Command.TrafficViolation
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
            //var model = await AggregateBus.GetExsitsAggregate<TrafficViolationModel>(command.TrafficViolationId);

            //command.CopyValueTo(model);

            //await model.ModifyAsync();
        }
    }
}
