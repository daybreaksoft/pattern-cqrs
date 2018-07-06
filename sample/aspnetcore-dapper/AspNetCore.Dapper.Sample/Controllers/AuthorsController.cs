using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Dapper.Sample.Command.Author;
using AspNetCore.Dapper.Sample.Data.Const;
using AspNetCore.Dapper.Sample.Domain.Aggregates;
using AspNetCore.Dapper.Sample.Query;
using AspNetCore.Dapper.Sample.Query.ViewModels;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNetCore.Dapper.Sample.Controllers
{
    public class AuthorsController : Controller
    {
        public async Task<IActionResult> Index([FromServices]AuthorQuery authorQuery)
        {
            ViewBag.Authors = await authorQuery.GetAuthors();

            return View();
        }

        public async Task<IActionResult> Edit([FromRoute]int? id, [FromServices] IAggregateBus aggregateBus, [FromServices] ConstQuery constQuery)
        {
            var sexSelectItems = await constQuery.GetSelectItems(ConstCategoryConst.Sex);
            ViewBag.SexSelectItems = sexSelectItems.Select(p => new SelectListItem(p.DisplayText, p.Id.ToString()));

            AuthorAggregate aggregate = null;

            if (id.HasValue)
            {
                // Load author aggregate
                aggregate = await aggregateBus.GetExsitsAggregate<AuthorAggregate>(id);
            }

            ViewBag.IsCreate = !id.HasValue;
            ViewBag.AuthorId = id;

            return View(aggregate);
        }

        public async Task<IActionResult> CreateCommand(CreateAuthorCommand command, [FromServices]ICommandBus commandBus)
        {
            await commandBus.SendAsync(command);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateCommand([FromRoute]int id, UpdateAuthorCommand command, [FromServices]ICommandBus commandBus)
        {
            command.Id = id;
            await commandBus.SendAsync(command);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteCommand([FromRoute]int id, [FromServices]ICommandBus commandBus)
        {
            await commandBus.SendAsync(new DeleteAuthorCommand { Id = id });

            return RedirectToAction("Index");
        }
    }
}
