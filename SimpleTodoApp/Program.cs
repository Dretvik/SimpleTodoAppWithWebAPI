using SimpleTodoApp.Model;

var builder = WebApplication.CreateBuilder(args); 
var app = builder.Build();

var inMemoryDb = new List<TodoItem>();

app.MapGet("/todo", () =>
{
    return inMemoryDb;
});
app.MapPost("/todo", (TodoItem todoItem) =>
{
    todoItem.Id = Guid.NewGuid();   // Her lager jeg en unik ID til hvert enkelt item, slik at jeg ikke får error når jeg sletter 
    inMemoryDb.Add(todoItem);       // et item jeg har satt til done og prøver å sette det neste itemet på samme index til done.
});
app.MapPut("/todo/{id}", (Guid id) =>
{
    var todoItem = inMemoryDb.SingleOrDefault(ti => ti.Id == id);
    todoItem.Done = DateTime.Today;
});
app.MapDelete("/todo/{id}", (Guid id) =>
{
    inMemoryDb.RemoveAll(ti => ti.Id == id);
});

app.UseStaticFiles();   // Må være med for å bruke "wwwroot" mappen og de statiske filene i den.
app.Run();
        
