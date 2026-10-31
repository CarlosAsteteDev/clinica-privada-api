using CitasMedicas.Api.Entidades;
using CitasMedicas.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CitasMedicas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitasController : ControllerBase
{
    private readonly CitaService citaService;

    public CitasController(CitaService citaService)
    {
        this.citaService = citaService;
    }

    [HttpGet]
    public ActionResult<List<Cita>> ObtenerTodas()
    {
        return Ok(citaService.ObtenerTodas());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Cita> ObtenerPorId(int id)
    {
        ValidarId(id);
        return Ok(citaService.ObtenerPorId(id));
    }

    [HttpPost]
    public ActionResult<Cita> Crear(Cita cita)
    {
        var citaCreada = citaService.Crear(cita);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = citaCreada.Id }, citaCreada);
    }

    [HttpPut("{id:int}")]
    public IActionResult Actualizar(int id, Cita cita)
    {
        ValidarId(id);
        citaService.Actualizar(id, cita);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Eliminar(int id)
    {
        ValidarId(id);
        citaService.Eliminar(id);
        return NoContent();
    }

    private static void ValidarId(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("El identificador debe ser mayor que cero.");
        }
    }
}
