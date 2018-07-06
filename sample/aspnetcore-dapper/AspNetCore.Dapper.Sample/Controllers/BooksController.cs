using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Dapper.Sample.Command.Book;
using AspNetCore.Dapper.Sample.Domain.Aggregates;
using AspNetCore.Dapper.Sample.Query;
using AspNetCore.Dapper.Sample.Query.ViewModels;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNetCore.Dapper.Sample.Controllers
{
    public class BooksController : Controller
    {
        public async Task<IActionResult> Index([FromServices]BookQuery bookQuery)
        {
            ViewBag.Books = await bookQuery.GetBooks();

            return View();
        }

        public async Task<IActionResult> Edit([FromRoute]int? id, [FromServices] IAggregateBus aggregateBus, [FromServices] BookTypeQuery bookTypeQuery, [FromServices] AuthorQuery authorQuery)
        {
            // Get book types as select items
            var bookTypes = await bookTypeQuery.GetBookTypesAsSelectItems();
            ViewBag.BookTypeSelectItems = bookTypes.Select(p => new SelectListItem(p.Type, p.Id.ToString()));

            // Get authors as select items
            var authors = await authorQuery.GetAuthorsAsSelectItems();
            ViewBag.AuthorSelectItems = authors.Select(p => new SelectListItem(p.Name, p.Id.ToString()));

            BookViewModel viewModel = null;

            if (id.HasValue)
            {
                // Load user
                var aggregate = await aggregateBus.GetExsitsAggregate<BookAggregate>(id);

                // Build view model
                viewModel = new BookViewModel();
                aggregate.CopyValueTo(viewModel, forcePropertyNames:new []{ "Id", "AuthorIds" });
            }

            ViewBag.IsCreate = !id.HasValue;
            ViewBag.AuthorId = id;

            return View(viewModel);
        }

        public async Task<IActionResult> CreateCommand(CreateBookCommand command, [FromServices]ICommandBus commandBus)
        {
            await commandBus.SendAsync(command);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateCommand([FromRoute]int id, UpdateBookCommand command, [FromServices]ICommandBus commandBus)
        {
            command.Id = id;
            await commandBus.SendAsync(command);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteCommand([FromRoute]int id, [FromServices]ICommandBus commandBus)
        {
            await commandBus.SendAsync(new DeleteBookCommand { Id = id });

            return RedirectToAction("Index");
        }
    }
}
