using Application.Participant.Interfaces;
using Application.Participant.Services;
using Application.Student.Interfaces;
using Application.Student.Services;
using Application.User.Interfaces;
using Application.User.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ISignUpService, SignUpService>(); 
builder.Services.AddScoped<ISignInService, SignInService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IParticipantService, ParticipantService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x => x.LoginPath = "/Sign/SignIn");


var services = builder.Services;

services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
        name: "SignUpRoute",
        pattern: "SignUp",
        defaults: new { controller = "Sign", action = "SignUp" }
    );

app.MapControllerRoute(
        name: "SignInRoute",
        pattern: "SignIn",
        defaults: new { controller = "Sign", action = "SignIn" }
    );

app.MapControllerRoute(
        name: "StudentProfile",
        pattern: "Profile",
        defaults: new { controller = "Profile", action = "StudentProfile" }
    );


app.MapControllerRoute(
        name: "ParticipantProfile",
        pattern: "Profile",
        defaults: new { controller = "Profile", action = "ParticipantProfile" }
    );

app.MapControllerRoute(
        name: "EventsRoute",
        pattern: "Events/Index"
    );

app.Run();