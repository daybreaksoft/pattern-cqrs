using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Daybreaksoft.Extensions.Functions;

namespace Daybreaksoft.Pattern.CQRS
{
    /// <summary>
    /// Defualt implemention of IDomainModelBuilder
    /// </summary>
    public class DefaultDomainModelBuilder : IDomainModelBuilder
    {
        protected readonly IDependencyInjection DI;

        public DefaultDomainModelBuilder(IDependencyInjection di)
        {
            DI = di;
        }

        /// <summary>
        /// Build new model via DI
        /// </summary>
        public TModel BuildModel<TModel>() where TModel : IDomainModel
        {
            return DI.GetService<TModel>();
        }

        /// <summary>
        /// Build new model via DI and set id
        /// </summary>
        public TModel BuildModel<TModel>(object id) where TModel : IDomainModel
        {
            var model = BuildModel<TModel>();

            model.Id = id;

            return model;
        }
    }
}
