using FileContext.Samples.FileSet.Api.Data;
using FileContext.Samples.FileSet.Api.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<AppDataContext>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

await app.Services.CreateScope().ServiceProvider.InitializeData();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
await app.RunAsync();