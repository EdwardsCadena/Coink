# Mi Proyecto API en .NET 6 con Arquitectura Limpia

Este proyecto es una API RESTful que proporciona funcionalidades para gestionar usuarios, países, departamentos y municipios. Está construido con .NET 6 y sigue los principios de la Arquitectura Limpia.

## Tecnologías y patrones de diseño utilizados

- .NET 6
- Entity Framework Core 6
- SQL Server
- AutoMapper
- Arquitectura Limpia
- Inyección de Dependencias
- Patrón de Repositorio

## Características

La API ofrece operaciones CRUD (Crear, Leer, Actualizar, Eliminar) en las siguientes entidades:

- Usuarios
- Países
- Departamentos
- Municipios

Cada usuario tiene asociado un país, un departamento y un municipio.

## Cómo usar

Cada entidad tiene su propio controlador con las operaciones CRUD correspondientes. Aquí se muestran algunos ejemplos de cómo usar la API:

- Obtener todos los usuarios: `GET /api/Users`
- Obtener un usuario por ID: `GET /api/Users/{id}`
- Crear un nuevo usuario: `POST /api/Users`
- Actualizar un usuario existente: `PUT /api/Users/{id}`
- Eliminar un usuario: `DELETE /api/Users/{id}`

Los mismos endpoints están disponibles para `Countries`, `Departments` y `Municipalities`.

## Validaciones

La API realiza diversas validaciones para asegurar la integridad de los datos:

- Al crear o actualizar un usuario, el `CountryId`, `DepartmentId` y `MunicipalityId` deben ser válidos y existir en la base de datos.
- Cada entidad tiene restricciones de longitud en varios campos para garantizar la calidad de los datos.

## Inyección de Dependencias

La API utiliza la inyección de dependencias para asegurar un acoplamiento débil y una alta cohesión entre las clases, lo que facilita la prueba y el mantenimiento del código.

## Arquitectura Limpia

El proyecto sigue los principios de la Arquitectura Limpia, lo que significa que la lógica de negocio y la lógica de la aplicación están separadas de la infraestructura y la interfaz de usuario. Esto hace que el sistema sea más testable, mantenible y adaptable a cambios.

## Futuras mejoras

- Implementar autenticación y autorización para proteger los endpoints de la API.
- Mejorar el manejo de errores y proporcionar mensajes de error más descriptivos.
- Añadir pruebas unitarias y de integración para asegurar la calidad del código.

## Contacto

Si tienes alguna pregunta o sugerencia, por favor, no dudes en contactarme.
