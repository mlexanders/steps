using Steps.Utils.AppDefinition;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDefinitions(builder, typeof(Program));

var app = builder.Build();

app.UseDefinitions(typeof(Program));

app.Run();