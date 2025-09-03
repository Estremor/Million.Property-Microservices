# Million Property Management - Microservices

Sistema de gestión de propiedades inmobiliarias desarrollado con arquitectura de microservicios en .NET 9, implementando patrones CQRS, DDD y autenticación JWT.

## 🏗️ Arquitectura

### Microservicios
- **Auth.API** - Servicio de autenticación y autorización
- **Properties.API** - Gestión de propiedades, propietarios e imágenes

### Tecnologías
- **.NET 9** - Framework principal
- **Entity Framework Core** - ORM para persistencia
- **MediatR** - Implementación CQRS
- **JWT Bearer** - Autenticación y autorización
- **FluentValidation** - Validación de modelos
- **AutoMapper** - Mapeo de objetos
- **Swagger/OpenAPI** - Documentación de APIs
- **Docker** - Containerización
- **SQL Server** - Base de datos

## 📁 Estructura del Proyecto

```
├── Auth.API/                     # API de Autenticación
├── Auth.Application/             # Lógica de aplicación (Commands/Handlers)
├── Auth.Domain/                  # Entidades y servicios de dominio
├── Auth.Infrastructure/          # Configuraciones EF y persistencia
├── Properties.API/               # API de Propiedades
├── Properties.Application/       # Lógica de aplicación CQRS
├── Properties.Domain/            # Entidades de propiedades
├── Properties.Infrastructure/    # Configuraciones EF
├── Shared.Domain/                # Componentes compartidos de dominio
├── Shared.Infrastructure/        # Infraestructura compartida
├── TestMillionandup.Property/    # Pruebas unitarias
└── docker-compose.yml            # Orquestación de contenedores
```

## 🚀 Funcionalidades

### Auth.API
- **POST** `/api/auth/get` - Autenticación de usuarios
- Generación de tokens JWT
- Validación de credenciales

### Properties.API
- **POST** `/api/property` - Crear nueva propiedad
- **PUT** `/api/property` - Actualizar propiedad completa
- **PATCH** `/api/property` - Actualizar solo precio
- **POST** `/api/owner` - Crear propietario
- **POST** `/api/image` - Subir imágenes de propiedades

## 🔐 Seguridad

- **JWT Authentication** - Tokens con expiración de 6 horas
- **Autorización por endpoints** - `[Authorize]` en controllers críticos
- **Validación de modelos** - FluentValidation en todas las entradas
- **SecurityKey compartida** - Entre microservicios para validación

## 🗄️ Base de Datos

### Entidades Principales
- **User** - Usuarios del sistema
- **Property** - Propiedades inmobiliarias
- **Owner** - Propietarios
- **PropertyImage** - Imágenes de propiedades
- **PropertyTrace** - Trazabilidad de cambios

### Conexiones
- **Auth**: `MillionAuthDb`
- **Properties**: `MillionPropertiesDb`

## 🐳 Ejecución con Docker

### Prerrequisitos
- Docker Desktop con Linux containers
- .NET 9 SDK (para desarrollo local)

### Comandos
```bash
# Levantar todos los servicios
docker-compose up --build

# Ejecutar en background
docker-compose up -d --build

# Ver logs específicos
docker-compose logs auth-api
docker-compose logs properties-api

# Detener servicios
docker-compose down
```

### URLs de Acceso
- **Auth API**: http://localhost:5001/swagger
- **Properties API**: http://localhost:5002/swagger
- **SQL Server**: localhost:1433 (sa/Million123!)

## 🧪 Pruebas

### Pruebas Unitarias
```bash
# Ejecutar localmente
dotnet test TestMillionandup.Property/

# Ver cobertura
dotnet test --collect:"XPlat Code Coverage"
```

### Credenciales de Prueba
- **Usuario**: Million.property
- **Contraseña**: M3110n

## 📖 Uso de APIs

### 1. Autenticación
```bash
POST http://localhost:5001/api/auth/get
{
  "userName": "Million.property",
  "password": "M3110n"
}
```

### 2. Crear Propiedad (requiere token)
```bash
POST http://localhost:5002/api/property
Authorization: Bearer {token}
{
  "name": "Casa Moderna",
  "address": "Calle 123 #45-67",
  "price": 500000,
  "codeInternal": "PROP001",
  "year": 2023,
  "ownerDocument": "12345678",
  "propertyImages": [
    {
      "file": "base64_image_data",
      "enabled": true
    }
  ]
}
```

## 🔧 Configuración

### Variables de Entorno
- `DbSetting__ConnectionString` - String de conexión a SQL Server
- `SecurityKey` - Clave para firma de tokens JWT
- `ASPNETCORE_ENVIRONMENT` - Entorno de ejecución

### Archivos de Configuración
- `appsettings.json` - Configuración base
- `appsettings.Development.json` - Configuración de desarrollo
- `launchSettings.json` - Configuración de puertos

## 📋 Patrones Implementados

- **CQRS** - Separación de comandos y consultas
- **DDD** - Domain-Driven Design
- **Repository Pattern** - Abstracción de persistencia
- **Dependency Injection** - Inversión de dependencias
- **Clean Architecture** - Separación por capas
- **Microservices** - Servicios independientes

## 🚧 Características Técnicas

- **Validación automática** con FluentValidation
- **Mapeo automático** con AutoMapper
- **Manejo de errores** centralizado con middleware
- **Documentación** automática con Swagger
- **Containerización** completa con Docker
- **Healthchecks** para servicios dependientes

## 👥 Equipo de Desarrollo

Desarrollado como prueba técnica para **Million Luxury Real Estate**

---

Para más información técnica, consultar la documentación de cada microservicio en sus respectivas carpetas.
