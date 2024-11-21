

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


//app.MapGet("/hello", () => "Hello named route")
//   .WithName("hi");

//app.MapGet("/", (LinkGenerator linker) =>
//        $"The link to the hello route is {linker.GetPathByName("hi", values: null)}");


//StudentEndpoints.Map(app);

app.MapGroup("/api/students")
      .MapStudentsApi()
      .WithTags("Student Api");



app.Run();


public static class RouteBuilderExtension
{
    public static RouteGroupBuilder MapStudentsApi(this RouteGroupBuilder group)
    {
        group.MapGet("/", async context =>
        {
            // Get all Students
            await context.Response.WriteAsJsonAsync(new { Message = "All Students...." });
        });


        group.MapPost("/", async context =>
        {
            // Get one Student 
            await context.Response.WriteAsJsonAsync(new { Message = "The Student After Insert" });
        });

        group.MapPut("/", async context =>
        {
            // Update one Student
            await context.Response.WriteAsJsonAsync(new { Message = "The Student After Update" });
        });

        group.MapDelete("/", async context =>
        {
            // Delete one Student
            await context.Response.WriteAsJsonAsync(new { Message = "Message After Delete" });
        });

        return group;
    }
}


public static class StudentEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/", async context =>
        {
            // Get all Students
            await context.Response.WriteAsJsonAsync(new { Message = "All Students...." });
        });


        app.MapPost("/", async context =>
        {
            // Get one Student 
            await context.Response.WriteAsJsonAsync(new { Message = "The Student After Insert" });
        });

        app.MapPut("/", async context =>
        {
            // Update one Student
            await context.Response.WriteAsJsonAsync(new { Message = "The Student After Update" });
        });

        app.MapDelete("/", async context =>
        {
            // Delete one Student
            await context.Response.WriteAsJsonAsync(new { Message = "Message After Delete" });
        });

    }
}