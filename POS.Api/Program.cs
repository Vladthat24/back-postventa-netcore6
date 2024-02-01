using POS.Aplication.Extensions;
using POS.Infraestructure.Extensions;
using POS.Api.Extensions;
using WatchDog;
using POS.Utilites.AppSettings;


var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

//Add serviuces to the container
var Cors = "Cors";

builder.Services.AddInjectionIntfraestructure(Configuration);
builder.Services.AddInjectionAplicacion(Configuration);
builder.Services.AddAuthentication(Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

//Configuracion de ClienteId Google
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("GoogleSettings"));

//Subir archivos local
builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: Cors,
        builder =>
        {
            builder.WithOrigins("*");
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors(Cors);

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseWatchDogExceptionLogger();

app.UseHttpsRedirection();

app.UseStaticFiles(); //Para visualizar imagenes

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.UseWatchDog(configuration =>
{
    configuration.WatchPageUsername = Configuration.GetSection("WatchDog:UserName").Value;
    configuration.WatchPagePassword = Configuration.GetSection("WatchDog:Password").Value;
});

app.Run();

public partial class Program { }
