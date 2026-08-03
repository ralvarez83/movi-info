using MediatR;
using Movies.Application.MovieFind;
using Movies.Application.MovieSearch;
using Movies.Domain;
using Movies.Infraestructure.MediatR.MovieFind;
using Movies.Infraestructure.TheMovieDb;
using Movies.Infraestructure.TheMovieDb.Configuration;
using Shared.Domain.Bus.Query;
using Shared.Infraestructure.Bus;

string  MyAllowSpecificOrigins = "MyAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Los PaaS (Render, Cloud Run, Fly...) inyectan el puerto a escuchar en la variable PORT
// y terminan el TLS en su propio proxy, por lo que aquí solo se escucha en HTTP.
string? port = Environment.GetEnvironmentVariable("PORT");

if (!String.IsNullOrEmpty(port)){
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
}

// builder.Configuration.GetSection(TheMovieDBOptions.Name).Bind(theMovieOptions);
//registrar servicio para la conexion

string[]? allowSpecificOrigins = builder.Configuration.GetSection(MyAllowSpecificOrigins).Get<string[]>();

string? frontEndHostName = Environment.GetEnvironmentVariable("FrontEndHostName");

if (!String.IsNullOrEmpty(frontEndHostName)){
    // Admite varios orígenes separados por comas, para poder añadir los deploy previews del front.
    string [] newAllowSpecificOrigins = frontEndHostName
        .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    if (null != allowSpecificOrigins){
        allowSpecificOrigins = [ ..allowSpecificOrigins, ..newAllowSpecificOrigins];
    }
    else{
        allowSpecificOrigins = newAllowSpecificOrigins;
    }
}

builder.Services.Configure<TheMovieDBOptions>(
    builder.Configuration.GetSection(TheMovieDBOptions.Name));

// El token de TheMovieDB nunca viaja en el código: se inyecta con la variable de entorno
// TheMovieDB__Authorisation. Si falta, se aborta el arranque en vez de servir 500 en cada request.
string? theMovieDBAuthorisation = builder.Configuration
    .GetSection(TheMovieDBOptions.Name)
    .GetValue<string>(nameof(TheMovieDBOptions.Authorisation));

if (String.IsNullOrWhiteSpace(theMovieDBAuthorisation)){
    throw new InvalidOperationException(
        "Falta el token de TheMovieDB. Defina la variable de entorno 'TheMovieDB__Authorisation' " +
        "(en local puede usar 'dotnet user-secrets set \"TheMovieDB:Authorisation\" \"<token>\"').");
}

builder.Services.AddTransient<MovieRespositoryConfiguration, ConfigTheMovieDBRespository> ();
builder.Services.AddTransient<MovieRepository, TheMovieDBRepository> ();
builder.Services.AddTransient<MovieFindById, MovieFindById> ();
builder.Services.AddTransient<MovieSearchByCriteria, MovieSearchByCriteria> ();
builder.Services.AddSingleton<Mediator,Mediator>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<MediatRFindByIdMovieQueryHandler>());
builder.Services.AddSingleton<QueryBus, MediatRQueryBus>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

bool hasCorsOrigins = null != allowSpecificOrigins && allowSpecificOrigins.Length > 0;

if (hasCorsOrigins){
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: MyAllowSpecificOrigins,
                        policy  =>
                        {
                            policy.WithOrigins(allowSpecificOrigins!);
                        });
    });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aplicar la política solo si se llegó a registrar: UseCors con una política inexistente
// lanza una excepción al recibir la primera petición.
if (hasCorsOrigins){
    app.UseCors(MyAllowSpecificOrigins);
}

// Sin UseHttpsRedirection: el TLS lo termina el proxy del hosting y el contenedor solo
// habla HTTP; redirigir aquí provocaría un bucle de redirecciones.
app.UseAuthorization();
app.MapControllers();

// Endpoint que usa el hosting para comprobar que la instancia está viva.
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.Run();

