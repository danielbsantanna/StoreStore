#🛒 StoreStore E-Commerce
##⚡ Overview
This is a microservices-based project that simulates a complete e-commerce environment. Each core business domain is implemented as an independent service, communicating with one another via asynchronous messaging. The architecture is orchestrated through an API Gateway using YARP, which centralizes routing and exposes Swagger/ReDoc documentation for all services. The project uses Docker Compose for orchestration, making it easy to run all services locally with a single command.

## 🧱 Architecture



											   [ React App ]
												     │
												     ▼
											  [ API Gateway ]
												     │
          ┌─────────────────────┬────────────────────┼───────────────────┬────────────────────┐
          ▼                     ▼                    ▼                   ▼                    ▼
    [ Order Service ] [ Customer Service  ]	[ Payment Service ] [ Product Service ]  [ Shipping Service ]
          │                     │                    │                   │                    │
          ├─────────────────────┴────────────────────┴───────────────────┴────────────────────┘
												   │    ▲
												   ▼    │
												[ RabbitMQ ]
                                                        



## 🚀 Features
- Users can place and track orders
- Orders are processed asynchronously via RabbitMQ
- Frontend receives real-time updates on order/payment status


##🖥️ Tech Stack

| Layer            | Tech                                |
|------------------|-------------------------------------|
| Frontend         | React, Node.js                      |
| Backend (C#)     | .NET 9.0, REST APIs                 |
| Real-time        | SignalR                             |
| Testing          | xUnit, Moq                          |
| Storage          | MongoDB                             |
| Documentation    | Swagger / ReDoc                     |
| Containerization | Docker, Docker Compose              |
| CI/CD            | GitHub Actions (build, test, deploy)|


## 🛠️ Pipeline Flow

1. **Push to Feature Branch**  
   Commits pushed to developer branch automatically trigger the pipeline.

2. **Run Unit Tests**  
   All backend services are built and tested using `xUnit` and `Moq`. Test failures prevent further steps.

3. **Build Test Docker Images**  
   If tests pass, the pipeline builds **Docker test images** for each service using the latest changes.

4. **Create Pull Request**  
   A pull request is automatically opened (or updated), signaling readiness for code review and team collaboration.

5. **Code Review and Merge**  
   Once approved, the PR will be merged into the main branch and production builds will be created.

##🧪 Install
After cloning the repository you can choose which version you will run

### Local build
```
    docker-compose -f docker-compose-dev.yml up
```

### Homolog build (Waiting for aprove)
```
    docker-compose -f docker-compose-dev.yml up
```

### Production build (Latests Release)
```
    docker-compose -f docker-compose-prod.yml up
```

After that you acess the GUI: `http://localhost:3000`

##🧾 Final Result
<div align="center">
  <img src="https://github.com/danielbsantanna/StoreStore/blob/main/OrderPanel.png" alt="Panel" width="50%" />
  <br>
</div>
---
✅ Roadmap / TODO
[ ] Deploy on the pipeline

[ ] Full centralized documentation

[ ] Complete unit tests

[ ] integration tests

[ ] Authentication

---
##👤 Author
Daniel Becker Sant'Anna

[E-mail](danielbsantanna@gmail.com)
[GitHub](https://github.com/danielbsantanna)
[Linkedin](https://www.linkedin.com/in/daniel-becker-sant-anna/)
