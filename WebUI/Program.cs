using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(
   )
    .AddCookie(option =>
    {
        option.LoginPath = "Account/Login";

        option.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        option.Cookie = new CookieBuilder
        {
            HttpOnly = true,
            Name = "Academy cookies",
            SameSite = SameSiteMode.Strict
        };

    });

builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = 21343447483648;
});

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Lockout.AllowedForNewUsers = true;
    options.User.RequireUniqueEmail = false;
    options.SignIn.RequireConfirmedEmail = true;

    //Aþaðýdaki alanlar isteðe göre aktifleþtirlebilir

    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.AllowedForNewUsers = true;

    options.User.RequireUniqueEmail = false;

    options.SignIn.RequireConfirmedEmail = true;
    options.SignIn.RequireConfirmedPhoneNumber = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{

    options.LoginPath = "/Account/Login";
    options.SlidingExpiration = true;
    options.AccessDeniedPath = new PathString("/Error/AccessDenied");
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

    options.Cookie = new CookieBuilder()
    {
        HttpOnly = true,
        Name = "Academy.Security.Cookie"
    };
});

Assembly.GetExecutingAssembly();
builder.Services.AddTransient<IEmailSender, EmailSenderManager>();
builder.Services.AddDbContext<AcademyContext>();
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<AcademyContext>()
    .AddDefaultTokenProviders();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();



builder.Services.AddMvc();
builder.Services.AddMvc(options =>
{
    options.EnableEndpointRouting = false;

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();
app.UseMvc(Route =>
{
    Route.MapRoute(
      name: "default",
      template: "{controller=Home}/{action=Index}/{id?}"
      );

    Route.MapRoute(
        name: "cart",
        template: "cart",
        defaults: new { controller = "Cart", action = "Index" }
        );
    Route.MapRoute(
        name: "exam",
        template: "exam",
        defaults: new { controller = "Exam", action = "Exam" }
        );
    Route.MapRoute(
       name: "result",
       template: "result",
       defaults: new { controller = "Result", action = "Index" }
       );
    Route.MapRoute(
     name: "sulution",
     template: "solution",
     defaults: new { controller = "Solution", action = "Questions" }
     );
    Route.MapRoute(
     name: "questions",
     template: "MyExam/{id?}",
     defaults: new { controller = "Exam", action = "Exam" }
     );
});

app.Run();
