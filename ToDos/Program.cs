using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Rewrite;
using Serilog;
using ToDos;

// WebAPI
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Dependency injection
builder.Services.AddSingleton<ITaskService>(new TaskService()); ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseRewriter(new RewriteOptions().AddRedirect("tasks/(.*)", "todos/$1"));


// Add logging middleware
app.Use(async (context, next) =>
{
    Log.Information("{Method} {Path} {Now} Started", context.Request.Method, context.Request.Path, DateTime.Now);
    await next(context);
    Log.Information("{Method} {Path} {Now} Finished", context.Request.Method, context.Request.Path, DateTime.Now);
});


app.MapGet("/todos", (ITaskService service) =>
{
    return service.GetToDos();
});

app.MapPost("/todos", (ToDo task, ITaskService service) =>
{
    var created = service.AddToDo(task);
    return TypedResults.Created("/todos/{id}", created);
}).AddEndpointFilter(async (context, next) =>
{
    var arg = context.GetArgument<ToDo>(0);
    var errors = new Dictionary<string, string[]>();

    if (arg.DueDate < DateTime.UtcNow)
    {
        errors.Add(nameof(ToDo.DueDate), ["DueDate cannot be in the past"]);
    }

    if (arg.IsCompleted)
    {
        errors.Add(nameof(ToDo.IsCompleted), ["Cannot add completed todo."]);
    }

    if (errors.Count > 0)
    {
        return Results.ValidationProblem(errors);
    }

    return await next(context);
});

app.MapGet("/todos/{id}", Results<Ok<ToDo>, NotFound> (int id, ITaskService service) =>
{
    var todo = service.GetToDo(id);
    return todo == null
    ? TypedResults.NotFound()
    : TypedResults.Ok(todo);
});

app.MapDelete("/todos/{id}", (int id, ITaskService service) =>
{
    service.DeleteToDo(id);
    return TypedResults.NoContent();
});

app.Run();