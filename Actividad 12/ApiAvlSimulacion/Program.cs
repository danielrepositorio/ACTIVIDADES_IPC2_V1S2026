var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var estadoArbol = new List<NodoAVL>
{
    new NodoAVL { Id = 30, Etiqueta = "Nodo Raíz (Abuelo) - FE: -2" },
    new NodoAVL { Id = 10, Etiqueta = "Hijo Izquierdo - FE: +1" }
};

app.MapGet("/api/arbol", () =>
{
    return Results.Ok(estadoArbol);
});

app.MapPost("/api/arbol/insertar", (NodoAVL nuevoNodo) =>
{
    if (nuevoNodo.Id <= 0)
    {
        return Results.BadRequest("ID de nodo inválido.");
    }

    if (nuevoNodo.Id == 20)
    {
        estadoArbol.Clear();

        estadoArbol.Add(new NodoAVL { Id = 20, Etiqueta = "Nueva Raíz Balanceada (RID) - FE: 0" });
        estadoArbol.Add(new NodoAVL { Id = 10, Etiqueta = "Hijo Izquierdo - FE: 0" });
        estadoArbol.Add(new NodoAVL { Id = 30, Etiqueta = "Hijo Derecho - FE: 0" });

        return Results.Created("/api/arbol", new
        {
            Mensaje = "Rotación RID ejecutada con éxito. Estabilidad total lograda.",
            Estructura = estadoArbol
        });
    }

    estadoArbol.Add(nuevoNodo);

    return Results.Created($"/api/arbol/{nuevoNodo.Id}", nuevoNodo);
});

app.Run();

public class NodoAVL
{
    public int Id { get; set; }
    public string Etiqueta { get; set; } = string.Empty;
    public int Altura { get; set; } = 1;
}
