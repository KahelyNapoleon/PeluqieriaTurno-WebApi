# PeluqueriaTurno-WebApi

1)API REST - Sistema de Gestión de Turnos:
API backend desarrollada en .NET para la gestión de turnos, clientes y servicios. 
Incluye autenticación y autorización mediante JWT Bearer y ASP.NET Core Identity.

2)Tecnologías utilizadas:
-.NET / ASP.NET Core
-C#
-Entity Framework Core
-SQL Server
-ASP.NET Core Identity
-JWT Bearer Authentication
-LINQ
-Git / GitHub
-Postman

3)Funcionalidades:
-CRUD completo de turnos, clientes y servicios
-Autenticación con JWT (login y generación de token)
-Autorización basada en roles (aun por implementar)
-Endpoints protegidos
-Manejo global de excepciones
-Arquitectura en capas (Controllers, Services, Repositories)
-Validación de datos

4)Arquitectura
El proyecto sigue una arquitectura en capas:
-Controllers: manejo de requests HTTP
-Services: lógica de negocio
-Repositories: acceso a datos
-DTOs: transferencia de datos
-Unit of Work: gestión de transacciones
Se utiliza Dependency Injection para desacoplar componentes.

5)Autenticación y Seguridad
Se implementa autenticación mediante JWT Bearer:
-Registro y login de usuarios
-Generación de tokens JWT
-Protección de endpoints mediante [Authorize] 
-Gestión de roles con ASP.NET Core Identity
-Se integró ASP.NET Core Identity con el modelo de negocio, extendiendo la entidad de usuario (AspNetUsers) y relacionándola con entidades del sistema, permitiendo gestionar la asociación entre usuarios autenticados y recursos como turnos y servicios.


##Estado:
Proyecto en mejora continua. Se siguen agregando funcionalidades y mejoras de seguridad, o dicho de otra manera aun no se ah finalizado.


