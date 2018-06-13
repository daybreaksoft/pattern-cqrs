using System;
using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;
using AspNetCore.Sample.Command.User;
using AspNetCore.Sample.Query.User;

namespace AspNetCore.Sample.Controllers
{
    public class UsersController : Controller
    {
        protected readonly ICommandBus CommandBus;

        public UsersController(ICommandBus commandBus)
        {
            CommandBus = commandBus;
        }

        public async Task<IActionResult> Index([FromServices]UserQuery userQuery)
        {
            ViewBag.Users = await userQuery.GetUsers();

            return View();
        }

        public async Task<IActionResult> Edit([FromRoute]int? id, [FromServices]IDomainModelBuilder modelBuilder)
        {
            UserViewModel viewModel = null;

            if (id.HasValue)
            {
                // Load user
                var userModel = modelBuilder.BuildModel<UserModel>(id);
                await userModel.LoadAsync();

                // Build view model
                viewModel = new UserViewModel();
                userModel.CopyValueTo(viewModel);
            }

            ViewBag.IsCreate = !id.HasValue;
            ViewBag.UserId = id;

            return View(viewModel);
        }

        public async Task<IActionResult> CreateCommand(CreateUserCommand command)
        {
            await CommandBus.SendAsync(command);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateCommand([FromRoute]int id, UpdateUserCommand command)
        {
            command.UserId = id;
            await CommandBus.SendAsync(command);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteCommand([FromRoute]int id)
        {
            await CommandBus.SendAsync(new DeleteUserCommand { UserId = id});

            return RedirectToAction("Index");
        }
    }
}
