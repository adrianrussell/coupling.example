using LowCouplingAPI.Application;
using LowCouplingAPI.Controllers;

namespace LowCouplingAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddTransient<IRepositoryBase, RepositoryBase>();
            builder.Services.AddControllers().AddJsonOptions(option => option.JsonSerializerOptions.Converters.Add(new MeasurementJsonConverter()));

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();
            
            app.MapControllers();

            app.Run();
        }
    }
}