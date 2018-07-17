using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.EF.Sample.Core.Const
{
    public class ConstEntity
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string DisplayText { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; }
    }
}
