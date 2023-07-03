using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Otus._2.DAL;
using Otus._2.Dto;
using Otus._2.Services;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets<Program>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UserService>();
builder.Services.AddDbContext<UserContext>(optionsBuilder =>
{
    optionsBuilder.UseNpgsql(builder.Configuration["DB_CONNECTION_STRING"]);
});

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpMetrics();
app.MapMetrics();

app.UseExceptionHandler(exceptionHandlerApp
    => exceptionHandlerApp.Run(async context
        =>
    {
        var exceptionHandlerPathFeature =
            context.Features.Get<IExceptionHandlerPathFeature>();
        await Results.Problem(title: exceptionHandlerPathFeature.Error.Message)
            .ExecuteAsync(context);
    }));

app.MapGet("api/User",
        async (int userId, [FromServices] UserService userService) => { return await userService.GetUser(userId); })
    .WithOpenApi().ProducesProblem(500);

app.MapPost("api/User",
    async (CreateUserDto user, [FromServices] UserService userService) =>
    {
        return await userService.CreateUser(user);
    }).WithOpenApi().ProducesProblem(500);

app.MapPut("api/User",
    async (UpdateUserDto user, [FromServices] UserService userService) =>
    {
        return await userService.UpdateUser(user);
    }).WithOpenApi().ProducesProblem(500);


app.MapDelete("api/User", async (int userId, [FromServices] UserService userService) =>
{
    await userService.DeleteUser(userId);
    return Results.StatusCode(204);
}).WithOpenApi().ProducesProblem(500);

app.Run();