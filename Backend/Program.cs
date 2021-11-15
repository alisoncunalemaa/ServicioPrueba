using Backend.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
builder.Services.AddDbContext<PublicacionesContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));
var app = builder.Build();

app.UseCors(options =>
{
    options.WithOrigins("http://localhost:5234");
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});
app.MapGet("/", () => "Hello World!");

app.MapGet("api/publicaciones", async (PublicacionesContext db) => await db.Publicacion.ToListAsync());
app.MapGet("api/publicaciones/{id}", async (PublicacionesContext db, int id) => await db.Publicacion.FindAsync(id));

app.MapPost("api/publicaciones/guardar", async (PublicacionesContext db, Publicaciones pub) =>
{
    await db.Publicacion.AddAsync(pub);
    await db.SaveChangesAsync();
    Results.Accepted();
});
app.MapPut("api/publicaciones/{id}", async (PublicacionesContext db, int id, Publicaciones pub) =>
{
    if(id != pub.Id) return Results.BadRequest();
    db.Update(pub);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("api/publicaciones/{id}", async (PublicacionesContext db, int id) =>
 {
     var pub = await db.Publicacion.FindAsync(id);
     if (pub == null) return Results.NotFound();
     db.Publicacion.Remove(pub);
     await db.SaveChangesAsync();
     return Results.NoContent();
 });

app.Run();
