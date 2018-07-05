using AspNetCore.Dapper.Sample.Data.Const;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Dapper.Sample.Command.Author
{
    public class CreateAuthorCommand : ICommand
    {
        public string Name { get; set; }

        public SexConst Sex { get; set; }
    }
}
