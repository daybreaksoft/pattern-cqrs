using System;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.EF.Sample.Command.User;
using AspNetCore.EF.Sample.Core.User;
using AspNetCore.EF.Sample.Data.Const;
using AspNetCore.EF.Sample.Data.Entities;
using AspNetCore.EF.Sample.Query;
using AspNetCore.EF.Sample.Query.ViewModels;
using Daybreaksoft.Pattern.CQRS.Command;
using Daybreaksoft.Pattern.CQRS.DomainModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNetCore.EF.Sample.Controllers
{
    public class UsersController : Controller
    {
        public async Task<IActionResult> Index([FromServices]UserQuery userQuery)
        {
            ViewBag.Users = await userQuery.GetUsers();

            return View();
        }

        public async Task<IActionResult> Edit([FromRoute]int? id, [FromServices] ConstQuery constQuery, [FromServices]IDomainService<UserModel> userService)
        {
            var roles = await constQuery.GetSelectItems(ConstCategory.UserRole);

            UserViewModel userViewModel = null;

            if (id.HasValue)
            {
                // Load user
                var userModel = await userService.FindAsync(id);

                userViewModel = new UserViewModel
                {
                    Id = Convert.ToInt32(userModel.Id),
                    Username = userModel.Username,
                    Point = userModel.Point,
                    //Roles = userModel.Roles.Select(p => (int)p.Role).ToArray()
                };
            }

            ViewBag.Roles = roles.Select(p => new SelectListItem(p.Text, p.Value));
            ViewBag.IsCreate = !id.HasValue;
            ViewBag.UserId = id;

            return View(userViewModel);
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
