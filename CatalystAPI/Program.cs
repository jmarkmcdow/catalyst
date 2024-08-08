using Microsoft.AspNetCore.StaticFiles;

var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpts => 
    {
        endpts.MapControllers();
    });

app.Run();
