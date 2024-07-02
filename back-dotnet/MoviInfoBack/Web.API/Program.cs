using Movies.Application.MovieFind;
using Movies.Application.MovieSearch;
using Movies.Domain;
using Movies.Infraestructure.TheMovieDb;
using Movies.Infraestructure.TheMovieDb.Configuration;

string  MyAllowSpecificOrigins = "MyAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// builder.Configuration.GetSection(TheMovieDBOptions.Name).Bind(theMovieOptions);
//registrar servicio para la conexion

string[] allowSpecificOrigins = builder.Configuration.GetSection(MyAllowSpecificOrigins).Get<string[]>();

string? frontEndHostName = Environment.GetEnvironmentVariable("FrontEndHostName");

if (!String.IsNullOrEmpty(frontEndHostName)){
    string [] newAllowSpecificOrigins = [frontEndHostName];
    if (null != allowSpecificOrigins){
        allowSpecificOrigins = [ ..allowSpecificOrigins, ..newAllowSpecificOrigins];
    }
    else{
        allowSpecificOrigins = newAllowSpecificOrigins;   
    }
}

builder.Services.Configure<TheMovieDBOptions>(
    builder.Configuration.GetSection(TheMovieDBOptions.Name));

builder.Services.AddTransient<MovieRespositoryConfiguration, ConfigTheMovieDBRespository> ();
builder.Services.AddTransient<MovieRepository, TheMovieDBRepository> ();
builder.Services.AddTransient<MoviFindById, MoviFindById> ();
builder.Services.AddTransient<MovieSearchByCriteria, MovieSearchByCriteria> ();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (null != allowSpecificOrigins){
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: MyAllowSpecificOrigins,
                        policy  =>
                        {
                            policy.WithOrigins(allowSpecificOrigins);
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


app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

