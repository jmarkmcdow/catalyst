
using Microsoft.AspNetCore.StaticFiles;
using Serilog;
using CatalsytAPI.Services;
using CatalystAPI.Interfaces;
using CatalsytAPI;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/contactinfo.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers(options => {
    //     Gets or sets the flag which decides whether an HTTP 406 Not Acceptable response
    //     will be returned if no formatter has been selected to format the response. false
    //     by default.
    options.ReturnHttpNotAcceptable = true;

    // Summary for AddXmlDataContractSerializerFormatters:
    //     Adds the XML DataContractSerializer formatters to MVC.
    // Returns:
    //     The Microsoft.Extensions.DependencyInjection.IMvcBuilder.
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

builder.Services.AddProblemDetails(options => {
    options.CustomizeProblemDetails = ctx => {
        ctx.ProblemDetails.Extensions.Add("server", Environment.MachineName);
    };
});

//     Provides a mapping between file extensions and MIME types.
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register my custom services
#if DEBUG
builder.Services.AddTransient<IMailService, LocalMailService>();
#else
builder.Services.AddSingleton<IMailService, CloudMailService>();
#endif
builder.Services.AddSingleton<ContactDataStore>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// defines a point in the middleware pipeline where routing decisions are made
app.UseRouting();

/*enables authorization capabilities.
When authorizing a resource that is routed using endpoint routing, 
this call must appear between the calls to app.UseRouting() and app.UseEndpoints(...) 
for the middleware to function correctly.*/
app.UseAuthorization();


#pragma warning disable ASP0014
app.UseEndpoints(endpts => 
    {
        endpts.MapControllers();
    });

app.Run();
