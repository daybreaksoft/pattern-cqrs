﻿using System;
using System.Threading.Tasks;
using AspNetCore.EF.Sample.Core.User;
using AspNetCore.EF.Sample.Core.Vehicle;
using Daybreaksoft.Pattern.CQRS.Command;
using Daybreaksoft.Pattern.CQRS.DomainModel;

namespace AspNetCore.EF.Sample.Command.Vehicle
{
    public class CreateVehicleCommandExecutor : ICommandExecutor<CreateVehicleCommand>
    {
        protected readonly IUnitOfWork UnitOfWork;

        public CreateVehicleCommandExecutor(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(CreateVehicleCommand command)
        {
            // Insert user
            int userId;

            if (!command.UserId.HasValue)
            {
                var user = new UserModel(command.Username, 0);

                await UnitOfWork.AddToStorageAsync(user);

                userId = Convert.ToInt32(user.Id);
            }
            else
            {
                userId = command.UserId.Value;
            }

            // Insert vehicle
            var vehicle = new VehicleModel(userId, command.PlateNumber);

            await UnitOfWork.AddToStorageAsync(vehicle);
        }
    }
}
