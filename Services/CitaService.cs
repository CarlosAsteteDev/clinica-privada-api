using CitasMedicas.Api.Entidades;

namespace CitasMedicas.Api.Services;

public class CitaService
{
    private static readonly List<Paciente> Pacientes =
    [
        new Paciente { Id = 1, Nombre = "Ana Torres", Documento = "12345678", Telefono = "999111222" },
        new Paciente { Id = 2, Nombre = "Luis Ramos", Documento = "87654321", Telefono = "999333444" }
    ];

    private static readonly List<Medico> Medicos =
    [
        new Medico { Id = 1, Nombre = "Carla Medina", Especialidad = "Medicina General" },
        new Medico { Id = 2, Nombre = "Jorge Salazar", Especialidad = "Pediatría" }
    ];

    private static readonly List<Cita> Citas = [];
    private static int siguienteId = 1;

    public List<Cita> ObtenerTodas()
    {
        return Citas
            .OrderBy(cita => cita.FechaHoraInicio)
            .ToList();
    }

    public Cita ObtenerPorId(int id)
    {
        return Citas.FirstOrDefault(cita => cita.Id == id)
            ?? throw new KeyNotFoundException("No existe una cita con el identificador indicado.");
    }

    public Cita Crear(Cita cita)
    {
        ValidarCita(cita, null);

        cita.Id = siguienteId;
        siguienteId++;
        Citas.Add(cita);

        return cita;
    }

    public void Actualizar(int id, Cita cita)
    {
        var citaExistente = ObtenerPorId(id);
        ValidarCita(cita, id);

        citaExistente.PacienteId = cita.PacienteId;
        citaExistente.MedicoId = cita.MedicoId;
        citaExistente.FechaHoraInicio = cita.FechaHoraInicio;
        citaExistente.DuracionMinutos = cita.DuracionMinutos;
        citaExistente.Motivo = cita.Motivo;
    }

    public void Eliminar(int id)
    {
        var cita = ObtenerPorId(id);
        Citas.Remove(cita);
    }

    private static void ValidarCita(Cita cita, int? idExcluido)
    {
        if (!Pacientes.Any(paciente => paciente.Id == cita.PacienteId))
        {
            throw new KeyNotFoundException("El paciente indicado no existe.");
        }

        if (!Medicos.Any(medico => medico.Id == cita.MedicoId))
        {
            throw new KeyNotFoundException("El médico indicado no existe.");
        }

        if (cita.FechaHoraInicio <= DateTime.Now)
        {
            throw new ArgumentException("La fecha y hora de la cita debe ser futura.");
        }

        var finNuevaCita = cita.FechaHoraInicio.AddMinutes(cita.DuracionMinutos);
        var existeCruce = Citas.Any(citaExistente =>
            citaExistente.MedicoId == cita.MedicoId &&
            citaExistente.Id != idExcluido &&
            cita.FechaHoraInicio < citaExistente.FechaHoraInicio.AddMinutes(citaExistente.DuracionMinutos) &&
            finNuevaCita > citaExistente.FechaHoraInicio);

        if (existeCruce)
        {
            throw new InvalidOperationException("El médico ya tiene una cita en ese horario.");
        }
    }
}
