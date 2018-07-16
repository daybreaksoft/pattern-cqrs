using System.Threading.Tasks;
using AspNetCore.EF.Sample.Command.User;
using AspNetCore.EF.Sample.Data;
using AspNetCore.EF.Sample.Query.User;
using Daybreaksoft.Pattern.CQRS.Command;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.EF.Sample.Controllers
{
    public class UsersController : Controller
    {
        public async Task<IActionResult> Index([FromServices]UserQuery userQuery)
        {
            ViewBag.Users = await userQuery.GetUsers();

            return View();
        }

        public async Task<IActionResult> Edit([FromRoute]int? id)
        {
            UserViewModel viewModel = null;

            if (id.HasValue)
            {
                //// Load user
                //var userModel = await aggregateBus.GetExsitsAggregate<User>(id);

                //// Build view model
                //viewModel = new UserViewModel();
                //userModel.CopyValueTo(viewModel);
            }

            ViewBag.IsCreate = !id.HasValue;
            ViewBag.UserId = id;

            return View(viewModel);
        }

        public async Task<IActionResult> CreateCommand(CreateUserCommand command, [FromServices]ICommandBus commandBus)
        {
            await commandBus.SendAsync(command);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateCommand([FromRoute]int id, UpdateUserCommand command, [FromServices]ICommandBus commandBus)
        {
            command.UserId = id;
            await commandBus.SendAsync(command);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteCommand([FromRoute]int id, [FromServices]ICommandBus commandBus)
        {
            await commandBus.SendAsync(new DeleteUserCommand { UserId = id });

            return RedirectToAction("Index");
        }
    }
}
