using System;
using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using AspNetCore.Sample.Repository.Entities;
using AspNetCore.Sample.ViewModels;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;
using AspNetCore.Sample.Command.User;

namespace AspNetCore.Sample.Controllers
{
    public class UsersController : Controller
    {
        protected readonly IRepository<User> UserRepository;
        protected readonly ICommandBus CommandBus;

        public UsersController(IServiceProvider serviceProvider, IRepository<User> userRepository, ICommandBus commandBus)
        {
            UserRepository = userRepository;
            CommandBus = commandBus;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Edit([FromRoute]int? id, [FromServices]IDomainModelBuilder modelBuilder)
        {
            UserViewModel viewModel = null;

            if (id.HasValue)
            {
                // Load user
                var userModel = new UserModel(UserRepository);
                userModel.Id = id;
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
    }
}
