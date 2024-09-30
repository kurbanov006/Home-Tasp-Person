using MainApp.ExtentionMethods;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
// builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.Register();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.UseRouting();

app.Run();

