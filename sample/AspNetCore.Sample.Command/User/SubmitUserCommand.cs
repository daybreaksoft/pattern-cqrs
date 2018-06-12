using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command.User
{
    public class SubmitUserCommand : ICommand
    {
        public string Username { get; set; }

        public int Point { get; set; }
    }
}
