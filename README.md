# NexusCommerce 🚀
**A Distributed, Event-Driven Microservices Ecosystem**

NexusCommerce is a modern e-commerce platform built using a microservices architecture. It demonstrates high-scale patterns like **Event-Driven Messaging**, **Transactional Outbox**, and **Distributed Tracing**.

## 🏗 System Architecture
The system consists of independent services communicating asynchronously via **RabbitMQ** and monitored via **OpenTelemetry**.

- **OrderService:** Manages customer orders using SQL Server and Entity Framework Core.
- **PaymentService:** Handles transaction processing (Event Consumer).
- **DeliveryService:** Manages shipping and logistics using MongoDB.
- **OrderGenerator:** A Bogus-powered console app that simulates high-traffic load.
- **SharedConfiguration:** A shared library for unified Telemetry and MassTransit setup.

## 🛠 Tech Stack
- **Framework:**  .NET 10
- **Messaging:** [MassTransit](https://masstransit-project.com/) with RabbitMQ
- **Database (Relational):** SQL Server (EF Core)
- **Database (NoSQL):** MongoDB
- **Observability:** OpenTelemetry (Traces & Metrics)
- **Monitoring Tools:** Grafana, Tempo, Jaeger
- **Testing:** Bogus (Data Generation)

## 🚦 Getting Started

### Prerequisites
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### 🚀 Running the entire solution
The easiest way to see the system in action is using Docker Compose:

```bash
# Clone the repository
git clone https://github.com/prasadpatil2666/MassFlow.git

# Navigate to the root folder
cd NexusCommerce

# Start all services and infrastructure
docker-compose up --build -d
