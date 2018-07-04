using System.Threading.Tasks;
using AspNetCore.Dapper.Sample.Command.BookType;
using AspNetCore.Dapper.Sample.Domain.Aggregates;
using AspNetCore.Dapper.Sample.Query;
using AspNetCore.Sample.Query;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Dapper.Sample.Controllers
{
    public class BookTypesController : Controller
    {
        public async Task<IActionResult> Index([FromServices]BookTypeQuery bookTypeQuery)
        {
            ViewBag.BookTypes = await bookTypeQuery.GetBookTypes();

            return View();
        }

        public async Task<IActionResult> Edit([FromRoute]int? id, [FromServices] IAggregateBus aggregateBus)
        {
            BookTypeViewModel viewModel = null;

            if (id.HasValue)
            {
                // Load user
                var userModel = await aggregateBus.GetExsitsAggregate<BookTypeAggregate>(id);

                // Build view model
                viewModel = new BookTypeViewModel();
                userModel.CopyValueTo(viewModel);
            }

            ViewBag.IsCreate = !id.HasValue;
            ViewBag.BookTypeId = id;

            return View(viewModel);
        }

        public async Task<IActionResult> CreateCommand(CreateBookTypeCommand command, [FromServices]ICommandBus commandBus)
        {
            await commandBus.SendAsync(command);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateCommand([FromRoute]int id, UpdateBookTypeCommand command, [FromServices]ICommandBus commandBus)
        {
            command.Id = id;
            await commandBus.SendAsync(command);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteCommand([FromRoute]int id, [FromServices]ICommandBus commandBus)
        {
          //  await commandBus.SendAsync(new DeleteUserCommand { UserId = id });

            return RedirectToAction("Index");
        }
    }
}
