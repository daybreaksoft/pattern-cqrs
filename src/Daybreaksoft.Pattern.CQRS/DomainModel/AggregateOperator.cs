using Daybreaksoft.Pattern.CQRS.Definition;

namespace Daybreaksoft.Pattern.CQRS.DomainModel
{
    public class AggregateOperator
    {
        public AggregateOperator(IAggregateRoot aggregate, AggregateAction action)
        {
            Aggregate = aggregate;
            Action = action;
        }

        public IAggregateRoot Aggregate { get; set; }

        public AggregateAction Action { get; set; }
    }
}
