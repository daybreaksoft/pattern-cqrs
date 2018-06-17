﻿using System.Threading.Tasks;
using AspNetCore.Sample.Domain.Models;
using Daybreaksoft.Pattern.CQRS;

namespace AspNetCore.Sample.Command
{
    public class DeleteVehicleCommandExecutor : ICommandExecutor<DeleteVehicleCommand>
    {
        protected readonly IAggregateBus AggregateBus;

        public DeleteVehicleCommandExecutor(IAggregateBus aggregateBus)
        {
            AggregateBus = aggregateBus;
        }

        public async Task ExecuteAsync(DeleteVehicleCommand command)
        {
            var model = AggregateBus.BuildAggregate<Vehicle>(command.VehicleId);

            model.Remove();

            await Task.CompletedTask;
        }
    }
}
