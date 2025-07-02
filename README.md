# Microservices Gateway Architecture

This repository demonstrates a full microservices architecture using multiple technologies (.NET, Go, Python) behind a reverse proxy gateway (YARP in .NET). The focus is on service communication, request routing, and structure—not on production-grade auth or persistence.

---

## 📦 Services Overview

| Service         | Tech     | Port | Purpose                   |
|-----------------|----------|------|---------------------------|
| Gateway         | .NET     | 5000 | Reverse proxy (YARP)      |
| Users Service   | .NET     | 6000 | Simulates user operations |
| Metrics Service | Go       | 7000 | Lightweight Go service    |
| Reports Service | Python   | 8000 | Lightweight Python service|

---

## 🧱 Project Structure

```
microservices/
├── users-service/          # .NET Minimal API
│   └── src/UsersService.Web/
│       ├── Program.cs
│       ├── Dockerfile
│       └── ...
├── metrics-service/        # Go service
│   └── src/MetricsService/
│       ├── main.go
│       ├── go.mod
│       └── Dockerfile
├── reports-service/        # Python service
│   └── src/ReportsService/
│       ├── main.py
│       └── Dockerfile
gateway/
└── src/Gateway.Web/        # .NET YARP Gateway
    ├── Program.cs
    ├── appsettings.json
    ├── yarp.dev.json
    ├── yarp.prod.json
    └── Dockerfile
```

---

## 🚀 Running Locally

### 1. Build and run all containers

```bash
docker-compose up --build
```

### 2. Available Endpoints

| Path                          | Proxied To                   |
|-------------------------------|------------------------------|
| `/api/v1/users/ping`         | Users Service (.NET)         |
| `/api/v1/metrics/ping`       | Metrics Service (Go)         |
| `/api/v1/reports/ping`       | Reports Service (Python)     |

Gateway Swagger (if enabled): `http://localhost:5000/api`

---

## 🛠 Dev Notes

- Gateway uses **YARP** for routing based on path segments.
- All services expose a `GET /api/v1/ping` route.
- No authentication/authorization included.
- Docker Compose sets up isolated services on different ports.

---

## 📄 License

This project is intended for educational and portfolio use only.
