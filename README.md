# MesaListo API - Backend

MesaListo API es el backend del proyecto **MesaListo**, una aplicación web tipo PWA orientada a la gestión de comunidades de juegos de mesa, eventos, noticias y participación de usuarios.

El backend está desarrollado en **.NET** bajo una estructura basada en **arquitectura hexagonal**, usando **SQL Server** como motor de base de datos y procedimientos almacenados para el acceso a datos.

---

## 1. Objetivo del backend

El objetivo de esta API es exponer los servicios necesarios para que el frontend de MesaListo pueda:

- Crear usuarios.
- Iniciar sesión.
- Consultar juegos de mesa precargados.
- Crear y consultar comunidades.
- Unirse y salir de comunidades.
- Crear y consultar noticias.
- Crear y consultar réplicas.
- Crear eventos.
- Consultar eventos próximos.
- Consultar eventos agendados.
- Consultar detalle de evento.
- Confirmar asistencia a eventos.
- Cancelar asistencia a eventos.

En esta fase del proyecto **no se implementa JWT todavía**.  
El frontend debe enviar temporalmente el `usuarioId` en los requests que lo requieran.

---

## 2. Tecnologías utilizadas

- .NET / ASP.NET Core Web API
- C#
- SQL Server
- Stored Procedures
- Swagger / OpenAPI
- Arquitectura hexagonal
- Angular como frontend consumidor
- PWA como enfoque de aplicación cliente

---

## 3. Estructura de la solución

La solución está organizada en cuatro proyectos principales:

```text
MesaListo.Application
MesaListo.Domain
MesaListo.Infrastructure
MesaListoAPI
