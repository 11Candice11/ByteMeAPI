using ByteMeAPI.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var filePath = builder.Configuration["ClientProfileFilePath"];
builder.Services.AddScoped<IClientProfileManager>(provider => new ClientProfileManager(filePath));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseCors(builder =>
    builder.WithOrigins("http://localhost:8080")
           .AllowAnyHeader()
           .AllowAnyMethod());
app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
