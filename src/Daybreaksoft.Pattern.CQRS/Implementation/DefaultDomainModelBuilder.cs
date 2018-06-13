using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Daybreaksoft.Extensions.Functions;

namespace Daybreaksoft.Pattern.CQRS
{
    public class DefaultDomainModelBuilder : IDomainModelBuilder
    {
        protected readonly IDependencyInjection DI;

        public DefaultDomainModelBuilder(IDependencyInjection di)
        {
            DI = di;
        }

        public TModel BuildModel<TModel>() where TModel : IDomainModel
        {
            return DI.GetService<TModel>();
        }

        public TModel BuildModel<TModel>(object id) where TModel : IDomainModel
        {
            var model = BuildModel<TModel>();

            model.Id = id;

            return model;
        }
    }
}
