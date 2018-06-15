﻿using System.ComponentModel.DataAnnotations.Schema;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Domain.Models
{
    public partial class User : AggregateRoot
    {
        [NotMapped]
        public override object Id => UserId;

        public virtual UserRole[] Roles { get; set; }

        #region Behaviors

        public void DeductPoint(int point)
        {
            this.Point = this.Point - point;
        }

        #endregion
    }
}