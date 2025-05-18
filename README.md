# 🏥 Scalable Medical System Backend

> **Production-grade backend built with Clean Architecture, DDD and CQRS— designed for real-world hospital systems.**

---

## 🚀 Overview

This backend powers a medical system capable of managing patients, checkups, diagnoses, and prescriptions — with **auditability, maintainability, and security** at its core. It is designed as a **modular monolith** that can be easily broken down into microservices.

> 🧠 Built by a full-stack developer diving deep into scalable backend architecture — this is my attempt to build a project and learn about all of its aspects.

---

## 🛠️ Tech Stack & Patterns

| Layer               | Tech / Pattern                                                                 |
|---------------------|--------------------------------------------------------------------------------|
| API                 | ASP.NET Core, REST, Swagger (OpenAPI + Scalar)                                 |
| Application Layer   | CQRS (Commands & Queries), Use Case Handlers                                   |
| Domain Layer        | Domain-Driven Design, Aggregates                                               |
| Infrastructure      | PostgreSQL, Entity Framework Core, Repository + Unit of Work                   |
| Cross-Cutting       | JWT Auth, Roles & Policies, Refresh Tokens, Logging, Stopwatch Profiling       |
| DevOps              | Dockerized, CI with GitHub Actions                                             |
| Testing             | xUnit – Unit & Integration Tests                                               |

---

## 🧍‍♂️ Key Features

- ✅ **Clean Architecture**: Fully decoupled layers for testability and scalability.
- ✅ **Domain-Driven Design**: Aggregates and rich domain models structure complex medical workflows.
- ✅ **CQRS**: Separation of read/write logic simplifies complex hospital use cases.
- ✅ **JWT Authentication & Policies**: Role and attribute-based access control.
- ✅ **Dynamic Configuration**: Environment-based settings for easy deployment.
- ✅ **Profiling & Observability**: Logs and stopwatch metrics for use case performance.
- ✅ **Microservice-Ready Design**: Modular layout, clean service boundaries, and pluggable infrastructure.
- ✅ **OpenAPI + Scalar**: Auto-generated documentation for all endpoints.
- ✅ **CI/CD-Ready**: GitHub Actions runs tasks on a schedule, Docker ensures portable builds.

---

## 🧪 Testing & Coverage

- ✔️ **Unit Tests** – All use cases tested with domain validation and business rule checks.
- ✔️ **Integration Tests** – Repositories, DB context behavior, and external integrations.

---

## 🔐 Security Model

- 🔑 JWT-based authentication  
- 👥 Role-based + attribute-based authorization policies  
- 🔄 Refresh token support with route-level protection  

---

## 💾 Database & Backup

- 📆 PostgreSQL with normalized schema  
- 🔄 Automated DB backup via background service  

---

## 📦 Microservice-Ready by Design

> This system was designed from day one to **scale out** as needed.

- ✅ Loose coupling via interfaces
- ✅ Decentralized use case logic
- ✅ Modular feature folders = microservice boundaries
- ✅ Repositories and DB contexts easily separable

## 🏁 Getting Started

```bash
# Clone & navigate
git clone https://github.com/vtyultinov/Hospital

# Start backend
docker-compose up --build

# Run tests
dotnet test
```

Access docs at: [http://localhost:5000/scalar/v1/](http://localhost:5000/scalar/v1/)

---

## 🔭 Roadmap

- [ ] Health checks + graceful shutdown
- [ ] Queued event dispatch (RabbitMQ/Kafka ready)
- [ ] Contribution guide

---

## 🧠 Author Notes

This full-stack backend project is built to explore real-world scalable architectures. Every decision in this repo is intentional, justified, and geared toward long-term maintainability.

