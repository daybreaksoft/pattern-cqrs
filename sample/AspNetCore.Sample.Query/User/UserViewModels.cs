using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Sample.Query.User
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public int Point { get; set; }
    }

    public class UserListItemViewModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public int Point { get; set; }
    }
}
