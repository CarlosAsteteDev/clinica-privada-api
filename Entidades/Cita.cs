using System.ComponentModel.DataAnnotations;

namespace CitasMedicas.Api.Entidades;

public class Cita
{
    public int Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "El paciente es obligatorio.")]
    public int PacienteId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "El médico es obligatorio.")]
    public int MedicoId { get; set; }

    public DateTime FechaHoraInicio { get; set; }

    [Range(15, 240, ErrorMessage = "La duración debe estar entre 15 y 240 minutos.")]
    public int DuracionMinutos { get; set; }

    [Required(ErrorMessage = "El motivo es obligatorio.")]
    [StringLength(500, MinimumLength = 3, ErrorMessage = "El motivo debe tener entre 3 y 500 caracteres.")]
    public string Motivo { get; set; } = string.Empty;
}
