using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using cp_02.Data;
using cp_02.Domain.Entity;
namespace cp_02.Controllers;

public static class VehicleController
{
    public static void MapVehicleEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Vehicle").WithTags(nameof(Vehicle));

        group.MapGet("/", async (cp_02Context db) =>
        {
            return await db.Vehicle.ToListAsync();
        })
        .WithName("GetAllVehicles")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Vehicle>, NotFound>> (Guid id, cp_02Context db) =>
        {
            return await db.Vehicle.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Vehicle model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetVehicleById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (Guid id, Vehicle vehicle, cp_02Context db) =>
        {
            var affected = await db.Vehicle
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.UserCancelID, vehicle.UserCancelID)
                    .SetProperty(m => m.IsCancel, vehicle.IsCancel)
                    .SetProperty(m => m.Id, vehicle.Id)
                    .SetProperty(m => m.LicensePlate, vehicle.LicensePlate)
                    .SetProperty(m => m.VehicleModel, vehicle.VehicleModel)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateVehicle")
        .WithOpenApi();

        group.MapPost("/", async (Vehicle vehicle, cp_02Context db) =>
        {
            db.Vehicle.Add(vehicle);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Vehicle/{vehicle.Id}", vehicle);
        })
        .WithName("CreateVehicle")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (Guid id, cp_02Context db) =>
        {
            var affected = await db.Vehicle
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteVehicle")
        .WithOpenApi();
    }
}
