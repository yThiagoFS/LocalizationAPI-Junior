using BaltaIoChallenge.WebApi.Data.v1;
using BaltaIoChallenge.WebApi.Data.v1.Contexts;
using BaltaIoChallenge.WebApi.Extensions.v1;
using BaltaIoChallenge.WebApi.Filters;
using BaltaIoChallenge.WebApi.Models.v1.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMvc(opts => opts.Filters.Add(new ExceptionFilter()));
builder.Services.AddEndpointsApiExplorer();
builder.AddSwagger();

builder.Configuration.LoadConfigurationValues();

builder.AddAuthentication();
builder.AddAuthServices();
builder.AddLocalizationServices();

var serverVersion = new MySqlServerVersion(new Version(10, 0, 6));

builder.Services.AddDbContext<AppDbContext>(opts => opts.UseMySql(builder.Configuration.GetConnectionString("MySqlConnectionString"), serverVersion));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseStatusCodePages(async context =>
{
    var statusCode = context.HttpContext.Response.StatusCode;

    if (statusCode == (int)HttpStatusCode.Unauthorized
        || statusCode == (int)HttpStatusCode.Forbidden)
    {
        await context.HttpContext.Response.WriteAsJsonAsync<ResponseDto<string>>(
            new ResponseDto<string>("You're not allowed to use this method. Try to login or register.", statusCode));
    }
    
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await UpdateDatabase();

app.Run();

async Task UpdateDatabase()
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (context.Database.GetPendingMigrations().Any())
        try
        {
            context.Database.BeginTransaction();
            context.Database.Migrate();
        }
        catch(Exception ex)
        {
            context.Database.RollbackTransaction();
            throw new Exception($"{ex.Message}");
        }

    await Database.SeedDbInitialValues(builder.Configuration.GetConnectionString("MySqlConnectionString")!);
}
