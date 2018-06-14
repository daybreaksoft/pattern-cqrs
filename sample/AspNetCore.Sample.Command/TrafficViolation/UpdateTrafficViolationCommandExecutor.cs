using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command.TrafficViolation
{
    public class UpdateTrafficViolationCommandExecutor : ICommandExecutor<UpdateTrafficViolationCommand>
    {
        protected readonly IDomainModelBuilder DomainModelBuilder;

        public UpdateTrafficViolationCommandExecutor(IDomainModelBuilder domainModelBuilder)
        {
            DomainModelBuilder = domainModelBuilder;
        }

        public async Task ExecuteAsync(UpdateTrafficViolationCommand command)
        {
            //// Load traffic violation
            //var model = DomainModelBuilder.BuildModel<Domain.Models.TrafficViolation>(command.VehicleId);
            //await model.LoadAsync();

            //// Copy value to model
            //command.CopyValueTo(model);

            //// Update traffic violation
            //await model.UpdateAsync();
        }
    }
}
