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
    todoItem.Id = Guid.NewGuid();   // Her lager jeg en unik ID til hvert enkelt item, slik at jeg ikke f�r error n�r jeg sletter 
    inMemoryDb.Add(todoItem);       // et item jeg har satt til done og pr�ver � sette det neste itemet p� samme index til done.
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

app.UseStaticFiles();   // M� v�re med for � bruke "wwwroot" mappen og de statiske filene i den.
app.Run();
        
