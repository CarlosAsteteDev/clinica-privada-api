# API de citas médicas

API REST en .NET 10 para registrar, consultar, actualizar y eliminar citas médicas. Los datos se conservan en listas en memoria y se reinician al detener la aplicación.

## Funcionalidades

- Registro, consulta, actualización y eliminación de citas médicas.
- Validación de los datos recibidos mediante DataAnnotations.
- Verificación de pacientes y médicos precargados.
- Prevención de horarios superpuestos para un mismo médico.
- Respuestas HTTP acordes al resultado de cada operación.

## Componentes de la solución

| Componente | Responsabilidad |
| --- | --- |
| `Program.cs` | Configura los controladores, el servicio de citas y el middleware de errores. |
| `Controllers/CitasController.cs` | Recibe las solicitudes HTTP y expone los endpoints de la API. |
| `Services/CitaService.cs` | Aplica la lógica CRUD y valida pacientes, médicos y horarios. |
| `Entidades/` | Define los modelos `Cita`, `Paciente`, `Medico` y la respuesta de error. |
| `Middleware/ExceptionHandlingMiddleware.cs` | Convierte excepciones de la aplicación en respuestas HTTP controladas. |
| `Postman/` | Contiene las colecciones y el entorno para pruebas funcionales. |

## Estructura del proyecto

```text
CitasMedicas.Api/
├── Controllers/
│   └── CitasController.cs
├── Entidades/
│   ├── Cita.cs
│   ├── Medico.cs
│   ├── Paciente.cs
│   └── RespuestaError.cs
├── Middleware/
│   └── ExceptionHandlingMiddleware.cs
├── Postman/
│   ├── CitasMedicas.Api.postman_collection.json
│   ├── CitasMedicas.Api.evidencias.postman_collection.json
│   └── CitasMedicas.Api.local.postman_environment.json
├── Properties/
│   └── launchSettings.json
├── Services/
│   └── CitaService.cs
├── Program.cs
├── README.md
└── CitasMedicas.Api.csproj
```

Una solicitud viaja desde Postman al controlador, luego al servicio de citas y finalmente a las listas en memoria. El resultado regresa como una respuesta HTTP con su código correspondiente.

## Requisitos

- SDK .NET 10.
- Postman para ejecutar la colección de pruebas.

## Ejecución

Desde la carpeta del proyecto:

```powershell
dotnet build
dotnet run
```

La configuración de inicio usa `http://localhost:5095`. Si la consola muestra otra dirección HTTP, actualiza `baseUrl` en el entorno de Postman antes de ejecutar las pruebas.

## Datos de referencia

| Tipo | Id | Nombre |
| --- | --- | --- |
| Paciente | 1 | Ana Torres |
| Paciente | 2 | Luis Ramos |
| Médico | 1 | Carla Medina |
| Médico | 2 | Jorge Salazar |

## Endpoints

| Método | Ruta | Resultado exitoso |
| --- | --- | --- |
| GET | `/api/citas` | 200 |
| GET | `/api/citas/{id}` | 200 |
| POST | `/api/citas` | 201 |
| PUT | `/api/citas/{id}` | 204 |
| DELETE | `/api/citas/{id}` | 204 |

Ejemplo para crear una cita:

```json
{
  "pacienteId": 1,
  "medicoId": 1,
  "fechaHoraInicio": "2030-12-10T09:00:00",
  "duracionMinutos": 30,
  "motivo": "Consulta de control"
}
```

La API devuelve 400 para datos inválidos, 404 cuando una cita, paciente o médico no existe, 409 si el médico tiene un horario superpuesto y 500 ante errores inesperados.

## Reglas de negocio

- El paciente y el médico deben existir en los datos precargados.
- La fecha de la cita debe ser futura.
- La duración permitida está entre 15 y 240 minutos.
- El motivo es obligatorio y debe tener entre 3 y 500 caracteres.
- Un médico no puede tener dos citas con horarios superpuestos.
- Las citas contiguas están permitidas.

## Códigos de respuesta

| Código | Uso en la API |
| --- | --- |
| 200 OK | Consulta realizada correctamente. |
| 201 Created | Cita registrada correctamente. |
| 204 No Content | Cita actualizada o eliminada correctamente. |
| 400 Bad Request | Datos de entrada inválidos. |
| 404 Not Found | Cita, paciente o médico no encontrado. |
| 409 Conflict | Horario superpuesto para el mismo médico. |
| 500 Internal Server Error | Error inesperado de la aplicación. |

## Pruebas con Postman

Para trabajar con Postman Cloud, importa estos archivos:

- `Postman/CitasMedicas.Api.postman_collection.json`: colección de siete solicitudes.
- `Postman/CitasMedicas.Api.evidencias.postman_collection.json`: colección ordenada de nueve solicitudes para las capturas del informe.
- `Postman/CitasMedicas.Api.local.postman_environment.json`: entorno con `baseUrl = http://localhost:5095`.

Después selecciona el entorno **Citas Médicas - Local** y abre cualquiera de las solicitudes para usar **Send**. Reinicia la API antes de ejecutar la colección de evidencias completa para partir de listas en memoria vacías.
