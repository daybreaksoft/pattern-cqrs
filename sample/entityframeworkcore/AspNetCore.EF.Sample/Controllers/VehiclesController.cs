using System.Linq;
using System.Threading.Tasks;
using AspNetCore.EF.Sample.Command.Vehicle;
using AspNetCore.EF.Sample.Data.Entities;
using AspNetCore.EF.Sample.Query;
using AspNetCore.EF.Sample.Query.ViewModels;
using Daybreaksoft.Pattern.CQRS.Command;
using Daybreaksoft.Pattern.CQRS.DomainModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNetCore.EF.Sample.Controllers
{
    public class VehiclesController : Controller
    {
        public VehiclesController()
        {
        }

        public async Task<IActionResult> Index([FromServices]VehicleQuery vehicleQuery)
        {
            ViewBag.Vehicles = await vehicleQuery.GetVehicles();

            return View();
        }

        public async Task<IActionResult> Edit([FromRoute]int? id, [FromServices] UserQuery userQuery, [FromServices] IDomainService<VehicleEntity> vehicleService)
        {
            // Get users as selectitem
            var users = await userQuery.GetUsers();

            // Load vehicle if edit
            VehicleViewModel vehicleModel = null;

            if (id.HasValue)
            {
                // Load vehicle
                var vehicle = await vehicleService.FindAsync(id);

                vehicleModel = new VehicleViewModel
                {
                    PlateNumber = vehicle.PlateNumber,
                    UserId = vehicle.UserId,
                };
            }

            ViewBag.IsCreate = !id.HasValue;
            ViewBag.VehicleId = id;

            var userListItems = users.Select(p => new SelectListItem(p.Username, p.Id.ToString())).ToList();
            userListItems.Insert(0, new SelectListItem("Please Select", ""));
            ViewBag.UserListItems = userListItems;

            return View(vehicleModel);
        }

        public async Task<IActionResult> CreateCommand(CreateVehicleCommand command, [FromServices] ICommandBus commandBus)
        {
            await commandBus.SendAsync(command);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateCommand([FromRoute]int id, UpdateVehicleCommand command, [FromServices] ICommandBus commandBus)
        {
            command.VehicleId = id;
            await commandBus.SendAsync(command);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteCommand([FromRoute]int id, [FromServices] ICommandBus commandBus)
        {
            await commandBus.SendAsync(new DeleteVehicleCommand { VehicleId = id });

            return RedirectToAction("Index");
        }
    }
}
