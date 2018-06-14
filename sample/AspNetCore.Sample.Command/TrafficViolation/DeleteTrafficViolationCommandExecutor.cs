using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command.TrafficViolation
{
    public class DeleteTrafficViolationCommandExecutor : ICommandExecutor<DeleteTrafficViolationCommand>
    {
        protected readonly IDomainModelBuilder DomainModelBuilder;

        public DeleteTrafficViolationCommandExecutor(IDomainModelBuilder domainModelBuilder)
        {
            DomainModelBuilder = domainModelBuilder;
        }

        public async Task ExecuteAsync(DeleteTrafficViolationCommand command)
        {
            //// Build Traffic violation
            //var model = DomainModelBuilder.BuildModel<Domain.Models.Vehicle>(command.TrafficViolationId);

            //// Remove Traffic violation
            //await model.RemoveAsync();
        }
    }
}
