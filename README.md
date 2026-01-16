
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

## 2️⃣ Análisis del Sistema Actual

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

## 3️⃣ Propuesta de Solución - Nueva Arquitectura
La arquitectura propuesta se basa en Clean Architecture combinada con CQRS, con el objetivo de lograr una solución desacoplada, mantenible y preparada para escalar. Clean Architecture permite organizar el sistema en capas claramente definidas, donde el dominio y las reglas de negocio permanecen independientes de frameworks, infraestructura y tecnologías externas, garantizando estabilidad ante cambios. Sobre esta base, CQRS separa explícitamente las responsabilidades de lectura y escritura, simplificando la lógica, mejorando la claridad del código y permitiendo optimizar cada flujo de forma independiente. Esta combinación facilita la testabilidad, reduce la deuda técnica del sistema legacy y habilita una migración progresiva hacia una arquitectura moderna, alineada con principios SOLID y preparada para escenarios cloud y de alta demanda.

<img width="1536" height="1024" alt="diagrama-arquitectura" src="https://github.com/user-attachments/assets/50217668-2d7c-4c03-9df1-11c883b82a2a" />

##
**Principios base:**

**Clean Architecture:** capas independientes y dependencias hacia el dominio.

**SOLID:** código extensible, mantenible y testeable.

**CQRS:** separación clara entre comandos y consultas.

## 4️⃣ OrderManagement.Api  

**API REST (.NET moderno)**
OrderManagement.Api es la capa de exposición del sistema, responsable de ofrecer las funcionalidades del dominio a través de APIs REST seguras y desacopladas. Actúa como el punto de entrada para aplicaciones cliente (web, mobile u otros sistemas), recibiendo solicitudes HTTP, validándolas y delegando su ejecución a la capa Application mediante comandos y consultas (CQRS). Esta capa no contiene lógica de negocio, limitándose a orquestar los casos de uso, manejar aspectos transversales como autenticación, autorización, versionado, manejo de errores y logging, garantizando así una comunicación clara, mantenible y preparada para integración con sistemas legacy y arquitecturas modernas.

### 🎯 Finalidad técnica
Exponer endpoints REST para la gestión de órdenes y autenticación, sirviendo como **fachada moderna** del sistema.

### 📌 Características principales
- Autenticación con JWT
- Endpoints REST (`/auth`, `/orders`)
- Manejo de errores HTTP (400, 401, 404, 500, etc)
- Integración con la capa Application
- No contiene lógica de negocio directa

### 🧠 Rol arquitectónico
Desacopla los clientes (Web, servicios, workers) de la lógica de negocio.

---

## 5️⃣ OrderManagement.Application  
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

## 6️⃣ OrderManagement.Domain  
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

## 7️⃣ OrderManagement.Infrastructure  
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

## 8️⃣ OrderManagement.Web  
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

## 9️⃣ OrderManagement.WcfService  
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

## 🔟 OrderManagement.Worker  
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

1. Descargar todo el repositorio de código fuente
2. Abrir el proyecto  con Visual Studio 2026: OrderManagement.Api/OrderManagement.Api.slnx
3. Establecer multiples proyectos de inicio
   <img width="800" height="544" alt="image" src="https://github.com/user-attachments/assets/6621be0b-0635-4ec0-aa71-c08434bd193a" />

5. Verificar que los proyectos:  **OrderManagement.Api** y **OrderManagement.Web** esten configurados como **Inicio**
6. Presionar **F5** para ejecutar ambos proyectos:
   - Cargará la una ventana de tu navegador predeterminado con la Web MVC del proyecto **OrderManagement.Web** en la URL: https://localhost:7061/
   <img width="832" height="309" alt="image" src="https://github.com/user-attachments/assets/8b4b3964-95fe-47d6-9712-f8609540a409" />

   - Y también cargará una ventana de comandos del proyecto backend API: **OrderManagement.Api** que contiene todos los Endpoints
   <img width="979" height="512" alt="image" src="https://github.com/user-attachments/assets/3f664655-8446-448f-ade2-b8bed64631c1" />

8. (Opcional) Ejecutar **OrderManagement.WcfService** si deseas probar el servicio WCF desarrollado.
9. (Opcional) Ejecutar **OrderManagement.Worker** si deseas ver la aplicación simulada de consola windows.

> Los proyectos legacy se ejecutan de forma independiente según necesidad.

---

## 🧪 Pruebas
Para poder probar las APIs debe estar corriendo los servidores IIS Express ejecutados previamente (**F5**) desde **Visual Studio**.

## APIs probadas vía Swagger y Postman
- Ingresar Swagger UI: [https://localhost:7006/swagger/index.html](https://localhost:7006/swagger/index.html)

  **Test Swagger -> POST /api/auth/login**
  <img width="1366" height="641" alt="image" src="https://github.com/user-attachments/assets/cfd85fe6-d930-4fe9-af48-182631f4a97e" />
  <img width="1366" height="641" alt="image" src="https://github.com/user-attachments/assets/a2100b3b-0e5c-4fde-ab0d-937a78a22a41" />

  **Test Postman -> GET /api/orders**
  URL: https://localhost:7006/api/orders?Cliente=Anthony Vidal&Desde=&Hasta&Page=1&PageSize=10
  Se debe ingresar el Token generado previamente, por ejemplo: Bearer {Token}
  <img width="1280" height="1040" alt="image" src="https://github.com/user-attachments/assets/8f4de086-9e53-47cd-ba09-102f456712f0" />

## Web ASP .Net MVC
- Ingresar a la URL: [https://localhost:7006/swagger/index.html](https://localhost:7061/Account/Login)
  - Digitar usuario: **admin** y password: **admin** 
  <img width="567" height="421" alt="image" src="https://github.com/user-attachments/assets/5237d86c-9690-44a9-9e05-9a01477d0889" />
  
  - Carga por defecto el listado de ordenes
  <img width="1145" height="334" alt="image" src="https://github.com/user-attachments/assets/1d43b7e7-de0c-4c27-afd5-dfe601e148ed" />

  - Crear nueva orden
  <img width="1234" height="446" alt="image" src="https://github.com/user-attachments/assets/a5401b35-8d2c-46f4-8b52-d1bd392ac349" />
  <img width="1132" height="371" alt="image" src="https://github.com/user-attachments/assets/e2211840-9d1b-4694-8a85-dbc417f4a5f0" />

  - Editar una orden  
  <img width="630" height="372" alt="image" src="https://github.com/user-attachments/assets/dccaf663-9489-4421-8f05-672b22a56d2b" />

  - Eliminar una orden
  <img width="655" height="293" alt="image" src="https://github.com/user-attachments/assets/6c2e82b6-2201-4106-b7b1-43ed0bbfc125" />
  <img width="1128" height="339" alt="image" src="https://github.com/user-attachments/assets/666c33a0-9aa1-4f9a-81a5-b257cb5b57c1" />




    



  

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

## 📦 Estretegia de Migración de Arquitectura
En mi experiencia profesional y tecnológica, considerando lo que indica textualmente el contexto del problema y asumiendo que el sistema actual tiene el tamaño de un ERP de una empresa mediana-grande. Plantearía una migración (Fase 1) progresiva de la arquitectura actual, que tiene tecnologías legacy como ASP.NET MVC 5, servicios WCF, ADO.NET y Windows Services, hacia una plataforma moderna en .NET, sustentada en principios de Clean Architecture, SOLID y CQRS. El enfoque recomendado prioriza la reducción de riesgo operativo y la continuidad del negocio, promoviendo una evolución controlada que desacople la lógica de negocio de las dependencias tecnológicas, facilite la mantenibilidad del sistema y siente las bases para una futura adopción de arquitectura cloud de forma segura y escalable. 

**En una Fase 2 pensar en migrar la infraestructura a la Nube con Azure o AWS.** 
---
## 🎯 Objetivo de la migración

Modernizar progresivamente una plataforma legacy:
- Sin detener la operación
- Sin “big bang rewrite”
- Reduciendo riesgo técnico
- Preparando el sistema para escala, nube y evolución futura

## 🧠 Estrategia RECOMENDADA: Strangler Fig Pattern
Se “envuelve” el sistema legacy y se lo va estrangulando funcionalmente, reemplazando piezas poco a poco.

**Ventajas:**
- Riesgo controlado
- Migración gradual
- Negocio sigue operando
- ROI visible desde etapas tempranas
---

## 🟢 FASE 1 – Modernización Controlada (On-Premise / Híbrida)
Desacoplar, ordenar y estabilizar antes de escalar.

## 🎯 Objetivo
Desacoplar, ordenar y estabilizar antes de escalar.

## 1️⃣ Extraer la lógica de negocio (PRIORIDAD 1)
**Acción**
- Identificar reglas de negocio críticas
- Moverlas a una capa:
  - Domain
  - Application
- Aplicar Clean Architecture y SOLID

**Resultado**
- El negocio deja de depender de MVC, WCF o ADO.NET
- Base sólida para cualquier frontend o backend

## 2️⃣ Introducir una API REST moderna
**Acción**
- Crear una API .NET moderna
- Usar CQRS para casos de uso críticos
- Mantener MVC 5 como cliente temporal

**Resultado**
- Frontend desacoplado
- Punto único de entrada
- Preparado para cloud

## 3️⃣ Adaptar WCF como integración legacy
**Acción**
- No eliminar WCF de inmediato
- Crear adaptadores:
  - WCF → Application
- Exponer REST paralelo

**Resultado**
- Coexistencia controlada
- Reducción progresiva de SOAP

## 4️⃣ Migrar ADO.NET → Entity Framework
**Acción**
- EF Core en Infrastructure
- Repositorios
- Unit of Work
- Mantener SPs críticos si es necesario

**Resultado**
- Código más mantenible
- Persistencia desacoplada
- Preparación para migrar DB

## 5️⃣ Reemplazar Windows Services
**Acción**
- Migrar lógica a:
  - Worker Services
  - Jobs desacoplados
- Mantener Windows Service solo como host temporal

**Resultado**
- Procesos modernos
- Fácil migración a cloud
---

## 🟢 FASE 2 – Cloud Enablement & Escalabilidad
Aquí recién se recomienda ir a la nube.

## 6️⃣ Preparar el sistema para la nube
**Acción**
- Externalizar configuración
- Secrets management
- Health checks
- Logging centralizado

## 7️⃣ Migrar gradualmente a Cloud
**Opciones**
- Azure App Service
- AWS ECS / Fargate
- GCP Cloud Run

**Enfoque**
- Lift & Improve (no Lift & Shift puro)

## 8️⃣ Reemplazar definitivamente WCF y Windows Services
**Acción**
- SOAP → REST / Async
- Windows Service → Cloud Worker / Queue

## 9️⃣ Optimizar con arquitectura moderna
*Opcional pero recomendado:*
- Mensajería (RabbitMQ / SQS / Service Bus)
- Cache distribuido
- Observabilidad
- Escalado automático

## 👤 Autor

**Anthony Vidal**  
Líder en Tecnología y Transformación Digital · Modernización de Sistemas  
18+ años de experiencia en TI
🔗 LinkedIn: https://www.linkedin.com/in/anthonyvidal/
