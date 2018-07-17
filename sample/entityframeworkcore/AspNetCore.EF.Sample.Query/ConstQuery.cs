using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.EF.Sample.Data;
using AspNetCore.EF.Sample.Data.Const;
using AspNetCore.EF.Sample.Query.ViewModels;
using Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.EF.Sample.Query
{
    public class ConstQuery : AbstractQuery<SampleDbContext>
    {
        public ConstQuery(SampleDbContext db) : base(db)
        {
        }

        public async Task<IEnumerable<SelectItemViewModel>> GetSelectItems(ConstCategory category)
        {
            var queryable = from c in Db.Const
                    where c.CategoryId == (int)category && c.Enabled
                    select new SelectItemViewModel
                    {
                        Text = c.DisplayText,
                        Value = c.Id.ToString()
                    };

            return await queryable.ToListAsync();
        }
    }
}
