# API de citas médicas

API REST en .NET 10 para registrar, consultar, actualizar y eliminar citas médicas. Los datos se conservan en listas en memoria y se reinician al detener la aplicación.

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

## Pruebas con Postman

Para trabajar con Postman Cloud, importa estos archivos:

- `Postman/CitasMedicas.Api.postman_collection.json`: colección de siete solicitudes.
- `Postman/CitasMedicas.Api.evidencias.postman_collection.json`: colección ordenada de nueve solicitudes para las capturas del informe.
- `Postman/CitasMedicas.Api.local.postman_environment.json`: entorno con `baseUrl = http://localhost:5095`.

Después selecciona el entorno **Citas Médicas - Local** y abre cualquiera de las solicitudes para usar **Send**. Reinicia la API antes de ejecutar la colección de evidencias completa para partir de listas en memoria vacías.
