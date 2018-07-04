﻿using Dapper.Contrib.Extensions;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Dapper.Sample.Data.Entities
{
    [Table("Books")]
    public class BookEntity : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int BookTypeId { get; set; }

        public string Name { get; set; }

        public int AuthorId { get; set; }
    }
}
