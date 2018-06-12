using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using AspNetCore.Sample.Models;
using AspNetCore.Sample.Repository.Entities;
using AspNetCore.Sample.ViewModels;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;
using Daybreaksoft.Pattern.CQRS.Extensions.EntityFrameworkCore;
using AspNetCore.Sample.Command.User;

namespace AspNetCore.Sample.Controllers
{
    public class UsersController : Controller
    {
        //protected readonly IDependencyInjection DI;

        //protected  readonly IServiceProvider

        protected readonly IRepository<User> UserRepository;
        protected readonly ICommandBus CommandBus;

        public UsersController(IServiceProvider serviceProvider, IRepository<User> userRepository, ICommandBus commandBus)
        {
            //DI = di;
            UserRepository = userRepository;
            CommandBus = commandBus;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Edit([FromQuery]int? id)
        {
            UserViewModel viewModel = null;

            if (id.HasValue)
            {
                // Load user
                var userModel = new UserModel(id, UserRepository);
                await userModel.LoadAsync();

                // Build view model
                viewModel = new UserViewModel();
                userModel.CopyValueTo(viewModel);
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Save([FromQuery]int? id, SubmitUserCommand command)
        {
            if (id.HasValue)
            {
                #region Update

                // Load user
                var userModel = new UserModel(id, UserRepository);
                await userModel.LoadAsync();

                // Copy value to model
                command.CopyValueTo(userModel);

                // Update user
                await userModel.UpdateAsync();

                #endregion
            }
            else
            {
                #region Create

                //// Create user model via command values
                //var userModel = new UserModel(UserRepository);
                //command.CopyValueTo(userModel);

                //// Insert user
                //await userModel.AddAsync();

                await CommandBus.SendAsync(command);

                #endregion
            }

            return RedirectToAction("Index");
        }
    }
}
