# UpNews - News Management Application

![.NET Version](https://img.shields.io/badge/.NET-7.0-blue)
![Node.js Version](https://img.shields.io/badge/Node-20.x-green)
![React Version](https://img.shields.io/badge/React-18.x-blue)

## Descripción
UpNews es una aplicación web completa para la gestión y consulta de noticias. Consta de tres componentes:

- **Panel Administrativo:** Gestión de noticias, usuarios, categorías y países con control de roles y autenticación por cookies.  
- **API REST:** Backend central que expone datos en formato JSON con lógica de negocio, seguridad y endpoints para consumo interno y externo.  
- **SPA Pública:** Interfaz interactiva y responsiva para consultar noticias, filtrarlas, ordenarlas, guardar favoritas y explorar secciones destacadas.


---


## Objetivos
- Gestionar y consultar noticias de manera segura y organizada.  
- Implementar control de acceso por roles (admin/editor).  
- Exponer API REST centralizada y documentada con `OpenAPI/Swagger`.  
- Ofrecer SPA pública interactiva y responsiva con filtros y favoritos.


---


## Alcance
**Incluye:** 
- Gestión de noticias, usuarios, categorías y países
- API REST con filtrado, búsqueda y workflow de publicación
- SPA pública con filtros y favoritos
- Autenticación y autorización por roles
- Documentación y buenas prácticas de desarrollo y CI/CD

**No incluye:** 
- Monetización, 
- interacción social avanzada
- notificaciones push


---



## Arquitectura
- **Modelo:** Entidades y datos (noticias, usuarios, categorías, países, favoritos).  
- **Vista:** Interfaz de usuario y presentación de datos.  
- **Controlador:** Gestiona la interacción entre modelo y vista, ejecutando la lógica de negocio.  

Arquitectura centralizada basada en `MVC `y `RESTful`.


---



## Stack Tecnológico

| Componente              | Tecnología                  | Versión / Notas                       |
|-------------------------|----------------------------|--------------------------------------|
| **Panel Administrativo** | ASP.Net Core MVC           | 7.0                                   |
|                         | C#                         | 11                                     |
|                         | Razor Pages / HTML/CSS     | N/A                                   |
|                         | Autenticación y Roles      | Cookie-based Auth                               |
| **API REST**            | Node.js                    | 20.x                                  |
|                         | Express.js                 | 4.18.x                                |
|                         | Testing                    | Jest 29 + Supertest 6                  |
|                         | Documentación              | OpenAPI 3.1 / Swagger UI 4.15          |
| **SPA Pública**         | React.js + Vite            | 18.x / Vite                           |
|                         | TailwindCSS                | 3.3.x                                 |
|                         | React Router DOM           | 6.14.x                                 |
|                         | Consumo de API             | Axios                                  |
|                         | Testing                    | Jest + Cypress 12.x                     |
| **DevOps / Herramientas** | Git / GitHub             | Última estable                        |
|                         | CI/CD                      | GitHub Actions                        |
|                         | Contenedores               | Docker, Docker Compose                |
|                         | IDE                        | VS Code 1.91 / Visual Studio 2022-2026 |
|                         | Despliegue (opcional)     | Azure, Kubernetes                       |
| **Infrastructure / Deploy** | Azure/Hetzner             | Según entorno                        |
|                         | Traefik                      | Latest Stable (Reverse Proxy)                        |
| **Datos / Persistencia** | MongoDB                    |7.0 / 7.x |
|                         | Redis    | 7.x (Caché en memoria)                       |


---


## Historial de Versiones
- 0.1
    - Dec 2025: Initial Release
