using System.Threading.Tasks;
using AspNetCore.EF.Sample.Command.User;
using AspNetCore.EF.Sample.Data.Entities;
using AspNetCore.EF.Sample.Query.User;
using Daybreaksoft.Pattern.CQRS.Command;
using Daybreaksoft.Pattern.CQRS.DomainModel;
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

        public async Task<IActionResult> Edit([FromRoute]int? id, [FromServices]IDomainAppService<UserEntity> userAppService)
        {
            UserEntity userModel = null;

            if (id.HasValue)
            {
                // Load user
                userModel = await userAppService.FindAsync(id);
            }

            ViewBag.IsCreate = !id.HasValue;
            ViewBag.UserId = id;

            return View(userModel);
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
