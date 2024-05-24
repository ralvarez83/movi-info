using WebAPI.Configurations;

string  MyAllowSpecificOrigins = "MyAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// builder.Configuration.GetSection(TheMovieDBOptions.Name).Bind(theMovieOptions);
//registrar servicio para la conexion

string[] allowSpecificOrigins = builder.Configuration.GetSection(MyAllowSpecificOrigins).Get<string[]>();

string? frontEndHostName = Environment.GetEnvironmentVariable("FrontEndHostName");

if (!String.IsNullOrEmpty(frontEndHostName)){
    string [] newAllowSpecificOrigins = [frontEndHostName];
    if (null != allowSpecificOrigins){
        allowSpecificOrigins = allowSpecificOrigins.Concat<string>(newAllowSpecificOrigins).ToArray<string>();
    }
    else{
        allowSpecificOrigins = newAllowSpecificOrigins;   
    }
}

builder.Services.Configure<TheMovieDBOptions>(
    builder.Configuration.GetSection(TheMovieDBOptions.Name));



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

