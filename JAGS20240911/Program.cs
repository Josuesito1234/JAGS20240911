var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

//Crear una lista para almacenar objetos de tipo Producto 
var productos = new List<Producto>();

//Configurar una ruta GET para obtener todos los productos
app.MapGet("/productos", () =>
{
return productos; // devuelve la lista de productos
});

//Configurar una ruta GET para obtener un producto especifico por su ID
app.MapGet("/productos/{id}", (int id) =>
{
//Busca un producto en la lista que tenga el ID especificado
var producto = productos.FirstOrDefault(c => c.Id == id);
return producto; //Devuelve el producto encontrado ( o null si no se encuentra)
});

//Configurar una ruta PUT para actualizar un producto existente por su ID
app.MapPut("/productos/{id}", (int id, Producto producto) => 
{
    var existingProducto = productos.FirstOrDefault(c =>c.Id ==id);
    if (existingProducto != null) 
    {
        //Actualiza los datos del cliente existente con los datos proporcionados
        existingProducto.Name = producto.Name;
        existingProducto.Precio = producto.Precio;
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }
});

//Configurar una ruta DELETE para eliminar un producto por su ID
app.MapDelete("/productos/{id}", (int id) =>
{
// Busca un producto en la lista que tenga un ID especificado
var existingProducto = productos.FirstOrDefault(c => c.Id == id);
if (existingProducto != null)
{
productos.Remove(existingProducto);
return Results.Ok(); // Devueve la respuesta HTTP 200 OK
}
else
{
return Results.NotFound(); // Devuelve una respuesta HTTP 404 Not Found si el producto no existe 
}
});

//Ejecutar la aplicacion
app.Run();

internal class Producto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Precio { get; set; }
}

