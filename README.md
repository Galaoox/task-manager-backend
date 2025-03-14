# Task Manager API

API REST para la gestión de tareas desarrollada con ASP.NET Core.

## Requisitos previos

- .NET 8.0 SDK o superior
- Visual Studio 2022, VS Code, o Rider (opcional)
- Docker (opcional)
- Make (opcional, para usar el Makefile)

## Opciones de ejecución

### 1. Usando Make (Recomendado)

```bash
# Compilar el proyecto
make build

# Ejecutar el proyecto
make run

# Ejecutar pruebas
make test

# Limpiar archivos compilados
make clean

# Ver todos los comandos disponibles
make help
```

### 2. Usando Docker

```bash
# Construir la imagen
make docker-build

# Ejecutar el contenedor
make docker-run

# O manualmente sin Make:
docker build -t task-manager-api .
docker run -p 7000:80 task-manager-api
```

### 3. Usando Visual Studio
1. Abre la solución `TaskManager.API.sln`
2. Configura el proyecto para usar HTTPS (puerto 7000)
3. Presiona F5 o Ctrl+F5 para ejecutar

### 4. Usando CLI de .NET
```bash
cd TaskManager.API
dotnet restore
dotnet build
dotnet run
```

## Estructura del proyecto
```
TaskManager.API/
│
├── Controllers/
│   └── TasksController.cs
│
├── Models/
│   └── Task.cs
│
├── Data/
│   ├── ITaskRepository.cs
│   └── InMemoryTaskRepository.cs
│
├── Program.cs
│
└── appsettings.json
```

## Endpoints API

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | /api/tasks | Obtener todas las tareas |
| GET | /api/tasks/{id} | Obtener una tarea por ID |
| POST | /api/tasks | Crear una nueva tarea |
| PUT | /api/tasks/{id} | Actualizar una tarea |
| DELETE | /api/tasks/{id} | Eliminar una tarea |

## Configuración

### CORS
Por defecto, el backend acepta conexiones desde `http://localhost:4200`. Para modificar esto, ajusta la configuración CORS en `Program.cs`.

### Puertos
- API: http://localhost:7000 (HTTPS)
- API: http://localhost:5000 (HTTP)

## Desarrollo

### Restaurar dependencias
```bash
dotnet restore
```

### Ejecutar pruebas
```bash
dotnet test
```

## Solución de problemas

### Error de certificado HTTPS
Si encuentras problemas con el certificado HTTPS en desarrollo:
```bash
dotnet dev-certs https --clean
dotnet dev-certs https --trust
```

### Problemas de CORS
Verifica que la URL del frontend coincida con la configuración CORS en `Program.cs`. 