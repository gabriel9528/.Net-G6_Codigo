using DataBaseFirst.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<G6DataBaseFirstContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("api/Clientes", async (G6DataBaseFirstContext _context) =>
{
    var listClients = new List<Cliente>();
    listClients = await _context.Clientes.AsNoTracking().ToListAsync();

    return listClients;
});

app.MapPost("api/Clientes", async (G6DataBaseFirstContext _context, Cliente cliente) =>
{
    _context.Clientes.Add(cliente);
    await _context.SaveChangesAsync();
    return Results.Created($"api/Clientes/{cliente.IdCliente}", cliente);
});

app.MapGet("api/TablaNueva", async (G6DataBaseFirstContext _context) =>
{
    var listTaleNew = new List<NewTable>();
    listTaleNew = await _context.NewTables.AsNoTracking().ToListAsync();

    return listTaleNew;
});

app.Run();
