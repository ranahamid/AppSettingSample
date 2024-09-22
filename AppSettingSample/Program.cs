using AppSettingSample;
using AppSettingSample.Models;

using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddConfiguration<SocialLoginSettings>(builder.Configuration, "SocialLoginSettings"); //configuration 
builder.Services.Configure<SocialLoginSettings>(builder.Configuration.GetSection("SocialLoginSettings")); //ioptions 


builder.Services.AddConfiguration<TwilioSettings>(builder.Configuration, "Twilio");//configuration 
builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection("Twilio")); //ioptions 

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    builder.Configuration.Sources.Clear();

    var env = hostingContext.HostingEnvironment;
    builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    builder.Configuration.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
    if (env.IsDevelopment())
    {
        builder.Configuration.AddUserSecrets<Program>();
    }
    builder.Configuration.AddEnvironmentVariables();
    builder.Configuration.AddCommandLine(args);

});




if (builder.Environment.IsProduction())
{
    builder.Configuration.AddAzureKeyVault(
        new Uri($"https://{builder.Configuration["KeyVaultName"]}.vault.azure.net/"),
        new DefaultAzureCredential());
}

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
