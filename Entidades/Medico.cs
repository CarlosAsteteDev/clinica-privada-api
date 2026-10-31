using System.ComponentModel.DataAnnotations;

namespace CitasMedicas.Api.Entidades;

public class Medico
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Especialidad { get; set; } = string.Empty;
}
