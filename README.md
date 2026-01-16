
---

## 📦 Modernización de un Módulo Crítico de Ventas - Sistema Legacy .NET Framework  
---
## 1️ Contexto General 
La empresa cuenta con una aplicación legado construida sobre:
- ASP.NET MVC 5 (con .NET Framework 4.7)
- Servicios WCF para intercambio de datos entre módulos
- ADO.NET y procedimientos almacenados en SQL Server
- Windows Services para tareas en segundo plano
La arquitectura presenta problemas de escalabilidad, mantenibilidad y extensibilidad. Se busca evaluar si el candidato está en capacidad de liderar un proceso de modernización desde el entendimiento del sistema actual, aplicando principios modernos y diseñando una solución sostenible.

## 1️ Análisis del Sistema Actual

## 2.1 Principales Problemas Identificados ⚠️

### Alto acoplamiento
- Controllers MVC contienen lógica de negocio.
- Dependencia directa a ADO.NET y SPs.
- Cambios pequeños generan impactos transversales.  

### Baja mantenibilidad y testabilidad
- Difícil aplicar pruebas unitarias.
- Lógica distribuida entre código y base de datos.
- WCF y Windows Services dificultan mocking y automatización.

### Limitaciones de escalabilidad
- Arquitectura monolítica.
- Escalamiento principalmente vertical.
- Windows Services no preparados para entornos cloud.

### Deuda técnica
- WCF es tecnología en desuso.
- .NET Framework limita adopción de nuevas capacidades (.NET moderno).
- Falta de separación clara de responsabilidades.

## 1️ Propuesta de Solución - Nueva Arquitectura
La arquitectura propuesta se basa en Clean Architecture combinada con CQRS, con el objetivo de lograr una solución desacoplada, mantenible y preparada para escalar. Clean Architecture permite organizar el sistema en capas claramente definidas, donde el dominio y las reglas de negocio permanecen independientes de frameworks, infraestructura y tecnologías externas, garantizando estabilidad ante cambios. Sobre esta base, CQRS separa explícitamente las responsabilidades de lectura y escritura, simplificando la lógica, mejorando la claridad del código y permitiendo optimizar cada flujo de forma independiente. Esta combinación facilita la testabilidad, reduce la deuda técnica del sistema legacy y habilita una migración progresiva hacia una arquitectura moderna, alineada con principios SOLID y preparada para escenarios cloud y de alta demanda.

<img width="1536" height="1024" alt="diagrama-arquitectura" src="https://github.com/user-attachments/assets/50217668-2d7c-4c03-9df1-11c883b82a2a" />

##
**Principios base:**

**Clean Architecture:** capas independientes y dependencias hacia el dominio.

**SOLID:** código extensible, mantenible y testeable.

**CQRS:** separación clara entre comandos y consultas.

## 1️ OrderManagement.Api  

**API REST (.NET moderno)**
OrderManagement.Api es la capa de exposición del sistema, responsable de ofrecer las funcionalidades del dominio a través de APIs REST seguras y desacopladas. Actúa como el punto de entrada para aplicaciones cliente (web, mobile u otros sistemas), recibiendo solicitudes HTTP, validándolas y delegando su ejecución a la capa Application mediante comandos y consultas (CQRS). Esta capa no contiene lógica de negocio, limitándose a orquestar los casos de uso, manejar aspectos transversales como autenticación, autorización, versionado, manejo de errores y logging, garantizando así una comunicación clara, mantenible y preparada para integración con sistemas legacy y arquitecturas modernas.

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

OrderManagement.Application representa la capa de aplicación y orquestación de casos de uso del sistema. Su responsabilidad principal es coordinar la ejecución de la lógica de negocio definida en el dominio, aplicando el patrón CQRS para separar claramente las operaciones de escritura (Commands) y lectura (Queries). En esta capa se definen los casos de uso, validaciones de negocio, reglas de flujo y contratos (interfaces) hacia servicios externos o infraestructura, sin depender de implementaciones concretas. Gracias a este enfoque, la capa Application actúa como el corazón funcional de la solución, garantizando alta testabilidad, bajo acoplamiento y alineación con los principios SOLID, además de facilitar la evolución y modernización progresiva del sistema legacy.

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

## 1️⃣ OrderManagement.Domain  
**Dominio del negocio (Core del sistema)**

OrderManagement.Domain representa el núcleo del negocio del sistema y contiene el modelo de dominio puro. En esta capa se definen las entidades, value objects e invariantes que gobiernan el comportamiento de las órdenes, completamente independientes de frameworks, bases de datos o interfaces de usuario. Su propósito es encapsular las reglas fundamentales del negocio y garantizar su consistencia, sirviendo como la fuente de verdad sobre cómo debe comportarse el sistema ante cualquier caso de uso.

### 🎯 Finalidad técnica
Representar el **modelo de dominio puro**, independiente de frameworks, bases de datos o UI.

### 📌 Características principales
- Entidades de dominio (`Order`, `OrderDetail`)
- Value Objects
- Reglas de negocio fundamentales
- Invariantes del dominio
- Sin dependencias externas

### 🧠 Rol arquitectónico
Es la **fuente de verdad del negocio**.  
No conoce ni API, ni MVC, ni WCF, ni base de datos.

---

## 3️⃣ OrderManagement.Infrastructure  
**Infraestructura y detalles técnicos**

OrderManagement.Infrastructure implementa los detalles técnicos de persistencia e integración definidos por la capa Application, incluyendo el acceso a datos mediante Entity Framework, la implementación concreta de repositorios y la configuración de contextos de base de datos. Esta capa actúa como adaptador entre el dominio y las tecnologías externas, encapsulando decisiones técnicas como el proveedor de base de datos, estrategias de mapeo y mecanismos de almacenamiento, de manera que la lógica de negocio permanece desacoplada de frameworks y detalles de infraestructura.

### 🎯 Finalidad técnica
Implementar los **detalles técnicos** definidos por la capa Application.

### 📌 Características principales
- Implementación de repositorios
- Acceso a datos (EF Core / simulación)
- Integraciones externas
- Persistencia
- Implementación de interfaces definidas en Application

### 🧠 Rol arquitectónico
Contiene lo que **puede cambiar** (DB, servicios externos, infraestructura).

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
