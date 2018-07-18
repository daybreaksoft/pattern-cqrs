using System.Linq;
using System.Threading.Tasks;
using AspNetCore.EF.Sample.Command.TrafficViolation;
using AspNetCore.EF.Sample.Core.TrafficViolation;
using AspNetCore.EF.Sample.Query;
using AspNetCore.EF.Sample.Query.TrafficViolation;
using Daybreaksoft.Extensions.Functions;
using Daybreaksoft.Pattern.CQRS;
using Daybreaksoft.Pattern.CQRS.Command;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNetCore.EF.Sample.Controllers
{
    public class TrafficViolationsController : Controller
    {
        protected readonly ICommandBus CommandBus;

        public TrafficViolationsController(ICommandBus commandBus)
        {
            CommandBus = commandBus;
        }

        public async Task<IActionResult> Index([FromServices]TrafficViolationQuery trafficViolationQuery)
        {
            ViewBag.TrafficViolations = await trafficViolationQuery.GetTrafficViolations();

            return View();
        }

        public async Task<IActionResult> Edit([FromRoute]int? id, [FromServices] VehicleQuery vehicleQuery)
        {
            // Get vehicles as selectitem
            var vehicles = await vehicleQuery.GetVehiclesAsSelectListItem();
            ViewBag.VehicleListItems = vehicles.Select(p => new SelectListItem(p.PlateNumber, p.Id.ToString()));

            // Load traffic violation if edit
            TrafficViolationViewModel viewModel = null;

            if (id.HasValue)
            {
                // Load traffic violation
                //var trafficViolationModel = await aggregateBus.GetExsitsAggregate<TrafficViolationModel>(id);

                //// Build view model
                //viewModel = new TrafficViolationViewModel();
                //trafficViolationModel.CopyValueTo(viewModel);
            }

            ViewBag.IsCreate = !id.HasValue;
            ViewBag.TrafficViolationId = id;

            return View(viewModel);
        }

        public async Task<IActionResult> CreateCommand(CreateTrafficViolationCommand command)
        {
            await CommandBus.SendAsync(command);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateCommand([FromRoute]int id, UpdateTrafficViolationCommand command)
        {
            command.VehicleId = id;
            await CommandBus.SendAsync(command);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteCommand([FromRoute]int id)
        {
            await CommandBus.SendAsync(new DeleteTrafficViolationCommand { TrafficViolationId = id });

            return RedirectToAction("Index");
        }
    }
}
