using System.Threading.Tasks;
using AspNetCore.EF.Sample.Core.TrafficViolation;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;
using Daybreaksoft.Pattern.CQRS.Command;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Command.TrafficViolation
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
            //var newModel = AggregateBus.BuildAggregate<TrafficViolationModel>();

            //command.CopyValueTo(newModel);

            //await newModel.AddAsync();
        }
    }
}
