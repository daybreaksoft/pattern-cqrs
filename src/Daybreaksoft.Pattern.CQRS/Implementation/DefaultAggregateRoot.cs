﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Daybreaksoft.Pattern.CQRS
{
    /// <summary>
    /// Abstract AggregateRoot
    /// </summary>
    public abstract class DefaultAggregateRoot : AggregateRoot
    {

        public virtual void Add()
        {
            _state = AggregateState.Added;
        }


        public virtual void Modify()
        {
            _state = AggregateState.Modified;
        }

        public virtual void Remove()
        {
            _state = AggregateState.Deleted;
        }
    }
}
