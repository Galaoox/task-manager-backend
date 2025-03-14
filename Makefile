.PHONY: build run test clean docker-build docker-run

# Variables
PROJ_DIR = TaskManager.API
DOCKER_IMAGE = task-manager-api
DOCKER_TAG = latest

# Comandos básicos de .NET
build:
	@echo "Compilando el proyecto..."
	cd $(PROJ_DIR) && dotnet build

run:
	@echo "Ejecutando el proyecto..."
	cd $(PROJ_DIR) && dotnet run

test:
	@echo "Ejecutando pruebas..."
	cd $(PROJ_DIR) && dotnet test

clean:
	@echo "Limpiando el proyecto..."
	cd $(PROJ_DIR) && dotnet clean
	rm -rf $(PROJ_DIR)/bin $(PROJ_DIR)/obj

# Comandos de Docker
docker-build:
	@echo "Construyendo imagen Docker..."
	docker build -t $(DOCKER_IMAGE):$(DOCKER_TAG) .

docker-run:
	@echo "Ejecutando contenedor Docker..."
	docker run -p 7000:8080 $(DOCKER_IMAGE):$(DOCKER_TAG)

# Comando de ayuda
help:
	@echo "Comandos disponibles:"
	@echo "  make build         - Compila el proyecto"
	@echo "  make run          - Ejecuta el proyecto localmente"
	@echo "  make test         - Ejecuta las pruebas"
	@echo "  make clean        - Limpia los archivos compilados"
	@echo "  make docker-build - Construye la imagen Docker"
	@echo "  make docker-run   - Ejecuta el contenedor Docker" 