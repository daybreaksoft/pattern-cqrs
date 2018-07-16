using System.Linq;
using System.Threading.Tasks;
using AspNetCore.EF.Sample.Command.Vehicle;
using AspNetCore.EF.Sample.Core.Vehicle;
using AspNetCore.EF.Sample.Query.User;
using AspNetCore.EF.Sample.Query.Vehicle;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;
using Daybreaksoft.Pattern.CQRS.Command;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNetCore.EF.Sample.Controllers
{
    public class VehiclesController : Controller
    {
        protected readonly ICommandBus CommandBus;

        public VehiclesController(ICommandBus commandBus)
        {
            CommandBus = commandBus;
        }

        public async Task<IActionResult> Index([FromServices]VehicleQuery vehicleQuery)
        {
            ViewBag.Vehicles = await vehicleQuery.GetVehicles();

            return View();
        }

        public async Task<IActionResult> Edit([FromRoute]int? id, [FromServices] UserQuery userQuery, [FromServices] IAggregateBus aggregateBus)
        {
            // Get users as selectitem
            var users = await userQuery.GetUsers();
            ViewBag.UserListItems = users.Select(p => new SelectListItem(p.Username, p.Id.ToString()));

            // Load vehicle if edit
            VehicleViewModel viewModel = null;

            if (id.HasValue)
            {
                // Load vehicle
                var vehicleModel = await aggregateBus.GetExsitsAggregate<VehicleModel>(id);

                // Build view model
                viewModel = new VehicleViewModel();
                vehicleModel.CopyValueTo(viewModel);
            }

            ViewBag.IsCreate = !id.HasValue;
            ViewBag.VehicleId = id;

            return View(viewModel);
        }

        public async Task<IActionResult> CreateCommand(CreateVehicleCommand command)
        {
            await CommandBus.SendAsync(command);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateCommand([FromRoute]int id, UpdateVehicleCommand command)
        {
            command.VehicleId = id;
            await CommandBus.SendAsync(command);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteCommand([FromRoute]int id)
        {
            await CommandBus.SendAsync(new DeleteVehicleCommand { VehicleId = id});

            return RedirectToAction("Index");
        }
    }
}
