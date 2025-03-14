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

## Justificación de la Estructura del Proyecto

La estructura del proyecto `TaskManager.API` está diseñada para seguir las mejores prácticas de desarrollo de software, facilitando la escalabilidad y el mantenimiento. A continuación, se describen las principales carpetas y su propósito:

- **Controllers/**: Contiene los controladores de la API, que manejan las solicitudes HTTP y definen los endpoints. Cada controlador se encarga de una entidad específica, en este caso, las tareas.

- **Models/**: Aquí se definen las clases que representan los datos de la aplicación. Incluye las clases de dominio como `TodoTask` y los DTOs (Data Transfer Objects) para la creación y actualización de tareas.

- **Data/**: Esta carpeta contiene las interfaces y las implementaciones relacionadas con el acceso a datos. `ITaskRepository` define las operaciones que se pueden realizar sobre las tareas, mientras que `InMemoryTaskRepository` es una implementación en memoria para pruebas y desarrollo.

- **Program.cs**: Este archivo es el punto de entrada de la aplicación. Aquí se configuran los servicios, middleware y la tubería de solicitudes HTTP.

- **appsettings.json**: Contiene la configuración de la aplicación, como los niveles de registro y los hosts permitidos.

Esta organización modular permite una fácil navegación y comprensión del código, así como la posibilidad de agregar nuevas funcionalidades sin afectar el resto del sistema.

## Documentación del Despliegue

El despliegue de la API `TaskManager` se realiza utilizando **GitHub Actions** y **Docker**, y se despliega en **Azure**. A continuación, se describen los pasos y la configuración necesaria para llevar a cabo el despliegue.

### Flujo de Trabajo de Despliegue

El archivo `.github/workflows/deployment.yml` define el flujo de trabajo para el despliegue automático de la aplicación en el entorno de producción. A continuación se detallan los pasos clave:

1. **Activación del Despliegue**: 
   - El flujo de trabajo se activa cuando hay un `push` a la rama `main`. Esto permite que cualquier cambio en la rama principal desencadene automáticamente el proceso de despliegue, asegurando que la versión más reciente de la aplicación esté siempre disponible.

2. **Construcción de la Imagen Docker**:
   - Se utiliza la acción `actions/checkout` para obtener el código fuente del repositorio. Esto es esencial para que el flujo de trabajo tenga acceso al código que se va a desplegar.
   - Se inicia sesión en Docker Hub utilizando las credenciales almacenadas en los secretos del repositorio. Esto es necesario para poder publicar la imagen Docker en el registro de Docker Hub.
   - Se construye la imagen Docker utilizando el `Dockerfile` presente en el proyecto. Esta imagen contiene toda la aplicación y sus dependencias, lo que permite que se ejecute en cualquier entorno que soporte Docker.

3. **Publicación de la Imagen Docker**: 
   - La imagen construida se publica en Docker Hub, lo que permite que sea accesible para su despliegue en otros entornos. Esto facilita la gestión de versiones y el acceso a la imagen desde diferentes plataformas.

4. **Despliegue en Azure**: 
   - Se utiliza la acción `azure/webapps-deploy` para desplegar la imagen en un servicio de aplicaciones de Azure. Este paso es crucial, ya que Azure proporciona un entorno escalable y seguro para ejecutar aplicaciones web.
   - Se requiere el nombre de la aplicación y el perfil de publicación, que también se almacenan en los secretos del repositorio. Esto asegura que el despliegue se realice de manera segura y sin exponer credenciales sensibles.

### Consideraciones

- Asegúrate de que las credenciales de Docker Hub y Azure estén correctamente configuradas en los secretos del repositorio para evitar errores durante el despliegue.
- Este flujo de trabajo permite un despliegue continuo, asegurando que los cambios en la rama `main` se reflejen automáticamente en el entorno de producción, lo que mejora la eficiencia y reduce el tiempo de inactividad.

## Documentación de los Secrets 

Para el correcto funcionamiento del flujo de trabajo de despliegue en GitHub Actions, es necesario configurar ciertos secretos en el repositorio. Estos secretos son utilizados para autenticar y autorizar el acceso a servicios externos, como Docker Hub y Azure. A continuación se detallan los secretos requeridos:

1. **DOCKERHUB_USERNAME**: 
   - Este secreto debe contener el nombre de usuario de tu cuenta de Docker Hub. Es necesario para iniciar sesión en Docker Hub y poder publicar la imagen Docker construida.

2. **DOCKERHUB_TOKEN**: 
   - Este secreto debe contener un token de acceso personal de Docker Hub. Este token se utiliza en lugar de la contraseña para autenticarte de manera segura en Docker Hub.

3. **AZURE_WEBAPP_PUBLISH_PROFILE**: 
   - Este secreto debe contener el perfil de publicación de tu aplicación en Azure. Puedes obtener este perfil desde el portal de Azure, en la sección de "Despliegue" de tu aplicación web. Este perfil incluye las credenciales necesarias para desplegar la aplicación en Azure.

### Cómo Configurar los Secrets en GitHub

Para agregar estos secretos a tu repositorio de GitHub, sigue estos pasos:

1. Ve a tu repositorio en GitHub.
2. Haz clic en la pestaña "Settings" (Configuración).
3. En el menú lateral, selecciona "Secrets and variables" y luego "Actions".
4. Haz clic en "New repository secret" para agregar cada uno de los secretos mencionados anteriormente.
5. Introduce el nombre del secreto (por ejemplo, `DOCKERHUB_USERNAME`) y su valor correspondiente, y luego haz clic en "Add secret".

Asegúrate de que estos secretos estén configurados correctamente para evitar errores durante el proceso de despliegue. 