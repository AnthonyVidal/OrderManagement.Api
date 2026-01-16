
---

## 📦 Proyectos de la Solución  
*(en el orden en que fueron construidos)*

---

## 1️ OrderManagement.Api  
**API REST (.NET moderno)**

### 🎯 Finalidad técnica
Exponer endpoints REST para la gestión de órdenes y autenticación, sirviendo como **fachada moderna** del sistema.

### 📌 Características principales
- Autenticación con JWT
- Endpoints REST (`/auth`, `/orders`)
- Manejo de errores HTTP (400, 401, 409)
- Integración con la capa Application
- No contiene lógica de negocio directa

### 🧠 Rol arquitectónico
Desacopla los clientes (Web, servicios, workers) de la lógica de negocio.

---

## 2 OrderManagement.Application  
**Capa de aplicación (Clean Architecture + CQRS)**

### 🎯 Finalidad técnica
Contener **los casos de uso y reglas de negocio del sistema**.

### 📌 Características principales
- Commands y Queries (CQRS)
  - `CreateOrderCommand`
  - `GetOrdersQuery`
- DTOs de entrada y salida
- Validaciones de negocio (no duplicar órdenes, reglas de detalle, etc.)
- Uso de MediatR

### 🧠 Rol arquitectónico
Es el **núcleo del sistema**.  
Aquí viven las reglas de negocio, independientes de UI, base de datos o frameworks.

---

## 3️ OrderManagement.Web  
**Aplicación Web MVC (ASP.NET Core MVC)**

### 🎯 Finalidad técnica
Interfaz web para usuarios finales, permitiendo login y CRUD de órdenes.

### 📌 Características principales
- Login con usuario/contraseña
- Consumo de API vía `HttpClient`
- Manejo de JWT en sesión
- Formularios Razor
- Mensajes de error de negocio sin exponer stacktrace
- Validaciones de UX sin duplicar reglas de negocio

### 🧠 Rol arquitectónico
Cliente web desacoplado que **consume contratos**, no lógica.

---

## 4 OrderManagement.WcfService  
**Servicio WCF (simulado – legacy)**

### 🎯 Finalidad técnica
Simular una **integración SOAP heredada**, típica en sistemas legacy que aún deben convivir con soluciones modernas.

### 📌 Características principales
- Proyecto WCF clásico (.NET Framework)
- Uso de:
  - `[ServiceContract]`
  - `[OperationContract]`
  - `[DataContract]`
- Método principal:
  - `RegistrarOrden`
- DTOs serializables (`OrdenDto`, `OrdenDetalleDto`)

### 🧠 Rol arquitectónico
Representa dependencias legacy que no pueden ser eliminadas de inmediato en un proceso de modernización.

---

## 5 OrderManagement.Worker  
**Windows Service simulado (Console App)**

### 🎯 Finalidad técnica
Simular un **servicio en segundo plano** que ejecuta tareas periódicas.

### 📌 Características principales
- Aplicación de consola
- Ejecución continua
- Tarea cada 30 segundos
- Simulación de procesamiento de órdenes
- Control de inicio y apagado (`Ctrl + C`)
- Compatible con C# 7.3 / .NET Framework

### 🧠 Rol arquitectónico
Representa procesos batch, jobs o servicios del sistema operativo típicos en entornos empresariales.

---

## 🔐 Seguridad y Validaciones

- Autenticación basada en JWT
- Las **reglas de negocio se validan solo en backend**
- El frontend **no replica reglas**
- Los errores técnicos no se exponen al usuario final
- Mensajes claros orientados a negocio

---

## 🧠 Principios y Patrones Aplicados

- Clean Architecture
- CQRS
- Separation of Concerns
- DTOs y contratos explícitos
- Legacy Modernization
- UX orientada a negocio
- Desacoplamiento entre capas

---

## ▶️ Cómo ejecutar la solución

1. Ejecutar **OrderManagement.Api**
2. Ejecutar **OrderManagement.Web**
3. (Opcional) Ejecutar **OrderManagement.WcfService**
4. (Opcional) Ejecutar **OrderManagement.Worker**

> Los proyectos legacy se ejecutan de forma independiente según necesidad.

---

## 🧪 Pruebas

- APIs probadas vía Swagger y Postman
- WCF probado con **WCF Test Client**
- Formularios MVC con validaciones de negocio
- Procesos batch simulados por consola

---

## 🏆 Enfoque del Proyecto

Este proyecto fue desarrollado como **ejercicio de evaluación técnica para el Grupo Tawa & Dinet**, demostrando:

- Capacidad de diseño arquitectónico
- Integración de sistemas legacy
- Buenas prácticas profesionales en .NET
- Pensamiento orientado a negocio y mantenibilidad

---

## 👤 Autor

**Anthony Vidal**  
Líder en Tecnología y Transformación Digital · Modernización de Sistemas  
18+ años de experiencia en TI
🔗 LinkedIn: https://www.linkedin.com/in/anthonyvidal/