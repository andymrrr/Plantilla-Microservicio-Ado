# Plantilla de Microservicio - Clean Architecture

## Descripción

Esta plantilla está diseñada para construir microservicios en .NET siguiendo los principios de **Clean Architecture**. La estructura del proyecto promueve la separación de responsabilidades, la mantenibilidad y el escalado, al mismo tiempo que implementa el patrón **CQRS** (Command Query Responsibility Segregation). orientada a usar ADO

## Características

- **Arquitectura modular y desacoplada** basada en Clean Architecture:
  - **Capa de Modelo**: Contiene los Modelos.
  - **Capa de Aplicación**: Implementa la lógica de caso de uso con CQRS.
  - **Capa de Dal**: Acceso a datos, servicios externos, y configuraciones específicas del framework.
  - **Capa Api**: API RESTful.
- Compatible con **.NET Core** y superior.
- Basada en principios SOLID.
- Configuración predefinida para evitar incluir carpetas innecesarias (`bin`, `obj`, `.git`, etc.).

## Instalación

### Requisitos previos

- **SDK de .NET 8.0 o superior**.
- Herramientas de desarrollo como Visual Studio, Visual Studio Code, o JetBrains Rider.
- Familiaridad con Clean Architecture y patrones CQRS.

### Instalación de la plantilla

1. Clona este repositorio o descarga los archivos.

```bash
  git clone https://github.com/andymrrr/Plantilla-Microservicio-Ado.git
```

2. Registra la plantilla localmente usando:

```bash
  dotnet new -i .
```

3. Verifica que la plantilla esté instalada:

```bash
  dotnet new --list
```

Deberías ver algo como esto:

```bash
micro-servicio  PlantillaMicroservicio   [C#]
```

## Crear un nuevo proyecto

```bash
dotnet new micro-servicio -n NombreDeTuProyecto
```

Esto creará un directorio con la estructura organizada según Clean Architecture.
