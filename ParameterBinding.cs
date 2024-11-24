//using Microsoft.AspNetCore.DataProtection.KeyManagement;
//using Microsoft.AspNetCore.Http.HttpResults;
//using Microsoft.AspNetCore.Http.Metadata;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Reflection;
//using System.Reflection.Metadata;
//using System.Security.Cryptography;
//using System.Xml.Linq;

//var builder = WebApplication.CreateBuilder(args);




//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();


//var app = builder.Build();



////1.Route Parameters
//app.MapGet("/developers/{id:int}", (int id) =>
//{
//    return TypedResults.Ok($"Fetching developer with ID: {id}");
//});


////2.Query String Parameters
//app.MapGet("/developers/search", ([FromQuery(Name = "developerName")] string? name) =>
//{
//    if (string.IsNullOrWhiteSpace(name))
//        return Results.NotFound();
//    return TypedResults.Ok(new { message = "Searching developer with name: {name}" });
//});

////3. Header Parameters
//app.MapGet("/developers/validate", ([FromHeader(Name = "X-API-Key")] string apiKey) =>
//{
//    if (string.IsNullOrWhiteSpace(apiKey) || apiKey != "12345")
//    {
//        return Results.Unauthorized();
//    }
//    return Results.Ok(new { Authorization = apiKey });
//});

////4.Body Parameters
//app.MapPost("/developers", (SoftwareDeveloper developer) =>
//{
//    return TypedResults.Ok($"Received developer: {developer.Name}, Specialization: {developer.Specialization}");
//});


////5. FromBody
//app.MapPost("/developer", ([FromBody] SoftwareDeveloper dev) =>
//{
//    var name = dev.Name;
//    var specialization = dev.Specialization;

//    return TypedResults.Ok($"Received developer: {name}, Specialization: {specialization}");
//});


////6.Form Parameters
//app.MapPost("/developers/upload", async (HttpContext context) =>
//{
//    var form = await context.Request.ReadFormAsync();
//    var name = form["name"].ToString();
//    var specialization = form["specialization"].ToString();

//    return TypedResults.Ok($"Received developer: {name}, Specialization: {specialization}");
//});


////7.Services Injection
//app.MapGet("/developers/service", (ILogger<Program> logger) =>
//{
//    logger.LogInformation("Service-based handler invoked.");
//    return TypedResults.Ok("Service injection is working!");
//});

////8.From Services
//app.MapGet("/developers/fromservices", ([FromServices] DeveloperService DevService) =>
//{
//    var welcome = DevService.GiveWelcomePackage();
//    return TypedResults.Ok($"welcome package means : {welcome}");
//});


////9.Raw HttpContext
//app.MapGet("/developers/raw", (HttpContext context) =>
//{
//    var requestMethod = context.Request.Method;
//    return TypedResults.Ok($"Request Method: {requestMethod}");
//});





//app.MapGet("/{id}", ([FromRoute] int id,
//                     [FromQuery(Name = "p")] int page,
//                     [FromServices] DeveloperService service,
//                     [FromHeader(Name = "Content-Type")] string contentType)
//                     => {

//                     }
//);


//app.UseSwagger();
//app.UseSwaggerUI();


//app.Run();





//public class DeveloperService
//{
//    public string GiveWelcomePackage()
//    {
//        return "Laptop and so many other things...";
//    }
//}
//public class SoftwareDeveloper
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public int Experience { get; set; }
//    public string? Specialization { get; set; }




//}