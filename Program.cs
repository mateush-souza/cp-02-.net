using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using cp_02.Data;
using cp_02.Controllers;
namespace cp_02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<cp_02Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("cp_02Context") ?? throw new InvalidOperationException("Connection string 'cp_02Context' not found.")));

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

                        if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.MapVehicleEndpoints();

            app.Run();
        }
    }
}
