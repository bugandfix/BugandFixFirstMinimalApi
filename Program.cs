using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);


//Service Registration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<ISoftwareDeveloperRepository, SoftwareDeveloperRepository>();
builder.Services.AddScoped<ISoftwareDeveloperService, SoftwareDeveloperService>();

var app = builder.Build();
//Middleware Registration Starts



#region EndPoints
app.MapGet("/developers", async (ISoftwareDeveloperService service, CancellationToken cancellationToken) =>
{
    var developers = await service.GetAllAsync(cancellationToken);
    return TypedResults.Ok(developers);
});

app.MapGet("/developers/{id:int}", async (int id, ISoftwareDeveloperService service, CancellationToken cancellationToken) =>
    await Task.FromResult<Results<Ok<SoftwareDeveloper>, NotFound<string>>>(await service.GetByIdAsync(id, cancellationToken) is SoftwareDeveloper developer
        ? TypedResults.Ok(developer)
        : TypedResults.NotFound($"Developer with ID {id} not found."))
);

app.MapPost("/developers", async (SoftwareDeveloper developer, ISoftwareDeveloperService service, CancellationToken cancellationToken) =>
{
    var addedDeveloper = await service.AddAsync(developer, cancellationToken);
    return TypedResults.Created($"/developers/{addedDeveloper.Id}", addedDeveloper);
});

app.MapPut("/developers/{id:int}", async (int id, SoftwareDeveloper developer, ISoftwareDeveloperService service, CancellationToken cancellationToken) =>
{
    if (id != developer.Id)
    {
        return Results.BadRequest("ID in route does not match ID in body.");
    }

    var updatedDeveloper = await service.UpdateAsync(developer, cancellationToken);
    return updatedDeveloper == null
        ? TypedResults.NotFound($"Developer with ID {id} not found.")
        : TypedResults.Ok(updatedDeveloper);
});

app.MapDelete("/developers/{id:int}", async (int id, ISoftwareDeveloperService service, CancellationToken cancellationToken) =>
{
    var success = await service.DeleteAsync(id, cancellationToken);
    return success
        ? TypedResults.Ok(true)
        : TypedResults.Ok(false);
});
#endregion







app.UseSwagger();
app.UseSwaggerUI();

//Middleware Registration Stops
app.Run();


#region Entity
public class SoftwareDeveloper
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Specialization { get; set; }
    public int Experience { get; set; }
}
#endregion

#region Abstraction
public interface ISoftwareDeveloperRepository
{
    Task<IEnumerable<SoftwareDeveloper>> GetAllAsync(CancellationToken cancellationToken);
    Task<SoftwareDeveloper?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<SoftwareDeveloper> AddAsync(SoftwareDeveloper developer, CancellationToken cancellationToken);
    Task<SoftwareDeveloper?> UpdateAsync(SoftwareDeveloper developer, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}


public interface ISoftwareDeveloperService
{
    Task<IEnumerable<SoftwareDeveloper>> GetAllAsync(CancellationToken cancellationToken);
    Task<SoftwareDeveloper?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<SoftwareDeveloper> AddAsync(SoftwareDeveloper developer, CancellationToken cancellationToken);
    Task<SoftwareDeveloper?> UpdateAsync(SoftwareDeveloper developer, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}

#endregion


#region Implementation

public class SoftwareDeveloperRepository : ISoftwareDeveloperRepository
{
    private readonly List<SoftwareDeveloper> _developers = new()
    {
        new SoftwareDeveloper { Id = 1, Name = "Ali", Specialization = "Backend", Experience = 5 },
        new SoftwareDeveloper { Id = 2, Name = "Reza", Specialization = "Frontend", Experience = 3 }
    };

    public async Task<IEnumerable<SoftwareDeveloper>> GetAllAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(1, cancellationToken); // Simulate async behavior
        return _developers;
    }

    public async Task<SoftwareDeveloper?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        await Task.Delay(1, cancellationToken); // Simulate async behavior
        return _developers.FirstOrDefault(d => d.Id == id);
    }

    public async Task<SoftwareDeveloper> AddAsync(SoftwareDeveloper developer, CancellationToken cancellationToken)
    {
        await Task.Delay(1, cancellationToken); // Simulate async behavior
        developer.Id = _developers.Any() ? _developers.Max(d => d.Id) + 1 : 1;
        _developers.Add(developer);
        return developer;
    }

    public async Task<SoftwareDeveloper?> UpdateAsync(SoftwareDeveloper developer, CancellationToken cancellationToken)
    {
        await Task.Delay(1, cancellationToken); // Simulate async behavior
        var existing = _developers.FirstOrDefault(d => d.Id == developer.Id);
        if (existing == null) return null;

        existing.Name = developer.Name;
        existing.Specialization = developer.Specialization;
        existing.Experience = developer.Experience;

        return existing;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await Task.Delay(1, cancellationToken); // Simulate async behavior
        var developer = _developers.FirstOrDefault(d => d.Id == id);
        if (developer == null) return false;

        _developers.Remove(developer);
        return true;
    }
}


//

public class SoftwareDeveloperService : ISoftwareDeveloperService
{
    private readonly ISoftwareDeveloperRepository _repository;

    public SoftwareDeveloperService(ISoftwareDeveloperRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<SoftwareDeveloper>> GetAllAsync(CancellationToken cancellationToken) =>
        _repository.GetAllAsync(cancellationToken);

    public Task<SoftwareDeveloper?> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        _repository.GetByIdAsync(id, cancellationToken);

    public Task<SoftwareDeveloper> AddAsync(SoftwareDeveloper developer, CancellationToken cancellationToken) =>
        _repository.AddAsync(developer, cancellationToken);

    public Task<SoftwareDeveloper?> UpdateAsync(SoftwareDeveloper developer, CancellationToken cancellationToken) =>
        _repository.UpdateAsync(developer, cancellationToken);

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken) =>
        _repository.DeleteAsync(id, cancellationToken);
}


#endregion