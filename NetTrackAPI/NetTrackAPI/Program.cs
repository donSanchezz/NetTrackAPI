using NetTrackAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using NetTrackAPI.Repositories.Auth;
using NetTrackAPI.ViewModels;
using NetTrackAPI.Repositories.Alert;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDirectoryBrowser();
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<FormOptions>(options => options.KeyLengthLimit = int.MaxValue);
builder.Services.AddSwaggerGen();
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNameCaseInsensitive = false;
});

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAlertRepository, AlertRepository>();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors(opts => opts.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddIdentity<User, IdentityRole>(

    opt =>
    {
        opt.Password.RequireDigit = false;
        opt.Password.RequireLowercase = false;
        opt.Password.RequireNonAlphanumeric = false;
        opt.Password.RequireUppercase = false;
        opt.Password.RequiredLength = 4;

        opt.User.RequireUniqueEmail = true;
        opt.SignIn.RequireConfirmedEmail = false;
    }).AddEntityFrameworkStores<ApplicationDbContext>().AddTokenProvider<DataProtectorTokenProvider<User>>("DataProtector");
builder.Services.AddAuthentication(cfg =>
{
    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Token:Key"])),
        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidateIssuer = true,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseStaticFiles();

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "Images")),
        RequestPath = "/Images"
    });

    app.UseDirectoryBrowser(new DirectoryBrowserOptions
    {
        FileProvider = new PhysicalFileProvider(
                  Path.Combine(Directory.GetCurrentDirectory(), "Images")),
        RequestPath = "/Images"
    });
}

//app.UseHttpsRedirection();

app.MapPost("/register", 
    (UserModel user, IAuthRepository repo) => Create(user, repo));

app.MapPost("/login",
    (LoginModel details, IAuthRepository repo) => Login(details, repo));


app.MapPost("/alert",
    (HttpRequest httpRequest, IAlertRepository repo) => Trigger(httpRequest, repo));


app.MapGet("/alert",
    (string userId, IAlertRepository repo) => GetAlert(userId, repo));

app.MapPost("/image",
    (HttpRequest httpRequest, IAlertRepository repo) => Upload(httpRequest, repo));

app.MapPost("/updatealert",
    (HttpRequest httpRequest, IAlertRepository repo) => UpdateAlert(httpRequest, repo));


app.MapPost("/stopalert",
    (HttpRequest httpRequest, IAlertRepository repo) => StopAlert(httpRequest, repo));

IResult Create(UserModel user, IAuthRepository repo)
{
    var result = repo.Create(user);
    if (result.Result == null)
    {
        return Results.NoContent();
    }
    return Results.Ok(result.Result);
}

IResult Login(LoginModel details, IAuthRepository repo)
{
    var result = repo.Login(details);
    if (result.Result == null)
    {
        return Results.NoContent();
    }
    return Results.Ok(result.Result);
}

IResult Trigger(HttpRequest httpRequest, IAlertRepository repo)
{
   

    var result = repo.Start(httpRequest);
    return Results.Ok();
}

IResult GetAlert(string userId, IAlertRepository repo)
{
    var result = repo.GetAlert(userId);
    if (result.Result == null)
    {
        return Results.NoContent();
    }
    return Results.Ok(result.Result);
}

IResult StopAlert(HttpRequest httpRequest, IAlertRepository repo)
{
    var result = repo.StopAlert(httpRequest);
    return Results.Ok();
}

IResult UpdateAlert(HttpRequest httpRequest, IAlertRepository repo)
{
    var result = repo.UpdateAlert(httpRequest);
    return Results.Ok();


}

IResult Upload(HttpRequest httpRequest, IAlertRepository repo)
{


    var result = repo.SaveImage(httpRequest);
    return Results.Ok();
}

app.Run();
