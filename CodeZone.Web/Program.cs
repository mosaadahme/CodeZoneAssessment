using AutoMapper;
using CodeZone.Core.Interfaces;
using CodeZone.Data;
using CodeZone.Data.Repositories;
using CodeZone.Services.Courses;
using CodeZone.Services.Enrollments;
using CodeZone.Services.Students;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

var builder = WebApplication.CreateBuilder ( args );

builder.Services.AddControllersWithViews ( );

builder.Services.AddDbContext<AppDbContext> ( options =>
    options.UseInMemoryDatabase ( "CodeZoneDb" ) );

builder.Services.AddScoped<IUnitOfWork, UnitOfWork> ( );

builder.Services.AddScoped<IStudentService, StudentService> ( );
builder.Services.AddScoped<ICourseService, CourseService> ( );
builder.Services.AddScoped<IEnrollmentService, EnrollmentService> ( );

var mapperConfig = new MapperConfiguration ( mc =>
{
    mc.AddMaps ( typeof ( StudentService ).Assembly );
    mc.AddMaps ( typeof ( CourseService ).Assembly );
    mc.AddMaps ( typeof ( EnrollmentService ).Assembly );
}, new NullLoggerFactory ( ) );

IMapper mapper = mapperConfig.CreateMapper ( );
builder.Services.AddSingleton ( mapper );

builder.Services.AddFluentValidationAutoValidation ( );
builder.Services.AddValidatorsFromAssemblyContaining<StudentService> ( );

var app = builder.Build ( );
app.UseMiddleware<CodeZone.Web.Middlewares.ExceptionMiddleware> ( );

using ( var scope = app.Services.CreateScope ( ) )
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext> ( );
        await DataSeeder.SeedAsync ( context );
    }
    catch ( Exception ex )
    {
        var logger = services.GetRequiredService<ILogger<Program>> ( );
        logger.LogError ( ex, "error exist" );
    }
}

if ( !app.Environment.IsDevelopment ( ) )
{
    app.UseExceptionHandler ( "/Home/Error" );
    app.UseHsts ( );
}

app.UseHttpsRedirection ( );
app.UseStaticFiles ( );

app.UseRouting ( );

app.UseAuthorization ( );

app.MapControllerRoute (
    name: "default",
    pattern: "{controller=Student}/{action=Index}/{id?}" );

app.Run ( );