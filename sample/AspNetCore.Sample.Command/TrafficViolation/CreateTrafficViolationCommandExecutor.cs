using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command.TrafficViolation
{
    public class CreateTrafficViolationCommandExecutor : ICommandExecutor<CreateTrafficViolationCommand>
    {
        protected readonly IDomainModelBuilder DomainModelBuilder;

        public CreateTrafficViolationCommandExecutor(IDomainModelBuilder domainModelBuilder)
        {
            DomainModelBuilder = domainModelBuilder;
        }

        public async Task ExecuteAsync(CreateTrafficViolationCommand command)
        {
            //// Create traffic violation model via command values
            //var model = DomainModelBuilder.BuildModel<Domain.Models.TrafficViolation>();
            //command.CopyValueTo(model);

            //// Insert traffic violation
            //await model.AddAsync();
        }
    }
}
