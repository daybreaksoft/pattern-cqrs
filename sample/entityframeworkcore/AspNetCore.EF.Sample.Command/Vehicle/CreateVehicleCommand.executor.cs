using System;
using System.Threading.Tasks;
using AspNetCore.EF.Sample.Core.User;
using AspNetCore.EF.Sample.Core.Vehicle;
using Daybreaksoft.Pattern.CQRS.Command;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Command.Vehicle
{
    public class CreateVehicleCommandExecutor : ICommandExecutor<CreateVehicleCommand>
    {
        private readonly IApplicationService<VehicleModel> _vehicleService;
        private readonly IApplicationService<UserModel> _userService;

        public CreateVehicleCommandExecutor(IApplicationService<VehicleModel> vehicleService, IApplicationService<UserModel> userService)
        {
            _vehicleService = vehicleService;
            _userService = userService;
        }

        public async Task ExecuteAsync(CreateVehicleCommand command)
        {
            // Insert new user if not selected exist user.
            int userId;

            if (!command.UserId.HasValue)
            {
                var user = new UserModel(command.Username, 0);

                await _userService.InsertAsync(user);

                userId = Convert.ToInt32(user.Id);
            }
            else
            {
                userId = command.UserId.Value;
            }

            // Insert vehicle.
            var vehicle = new VehicleModel(userId, command.PlateNumber);

            await _vehicleService.InsertAsync(vehicle);
        }
    }
}
