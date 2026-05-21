# Webhook API

## Descripción

Webhook API es una API desarrollada en C# utilizando ASP.NET Core, diseñada para recibir información de vehículos mediante Webhooks desde sistemas externos como CRM y otro tipo de plataformas como GPS, sistemas de monitoreo o flotas.

El objetivo principal del proyecto es demostrar una arquitectura desacoplada y capaz de resistir, adaptarse y recuperarse ante fallos, esto para evitar pérdida de información durante el procesamiento de registros en producción.

La solución fue diseñada pensando en escenarios empresariales donde múltiples registros son enviados por lotes y requieren persistencia, trazabilidad y procesamiento asíncrono.

---

# ¿Qué es un Webhook?

Un Webhook es un mecanismo de comunicación entre sistemas basado en eventos.

En lugar de que una aplicación consulte constantemente si existen cambios o nueva información, el sistema origen envía automáticamente los datos hacia un endpoint HTTP cuando ocurre un evento específico.

## Ejemplo

- Registro de un nuevo vehículo
- Actualización de ubicación GPS
- Cambio de estado de unidad
- Sincronización CRM
- Actualización de telemetría

---

# Problemática

En muchos entornos productivos, procesar directamente la información recibida desde un Webhook puede provocar:

- Pérdida de información
- Timeouts
- Saturación de servicios
- Fallos de CRM/ERP
- Duplicados
- Bloqueos de procesamiento

Por esta razón, el proyecto implementa un enfoque desacoplado basado en persistencia temporal (queue/staging) y procesamiento asíncrono.

---

# Consideraciones del Ejercicio

Por temas de tiempo y enfoque del ejercicio técnico, la implementación actual se limitó a demostrar:

- Recepción del Webhook
- Arquitectura desacoplada
- Procesamiento batch
- Validaciones básicas
- Simulación de queue/staging en memoria
- Estructura enterprise del proyecto

La conexión real hacia SQL Server y la persistencia mediante Entity Framework Core quedaron planteadas dentro de la arquitectura propuesta, pero no fueron implementadas completamente en esta versión.

El objetivo principal fue priorizar el diseño de integración, resiliencia y manejo correcto del flujo de Webhooks antes de la infraestructura de persistencia definitiva.

La solución fue diseñada para que posteriormente pueda integrarse fácilmente con:

- SQL Server
- Entity Framework Core
- RabbitMQ
- Azure Service Bus
- Procesamiento asíncrono real

# Arquitectura del Proyecto

```text
Sistema Externo
       ↓
WebhookController
       ↓
VehicleService
       ↓
VehicleWebhookQueue (Staging)
       ↓
BackgroundService
       ↓
SQL Server / CRM


---

