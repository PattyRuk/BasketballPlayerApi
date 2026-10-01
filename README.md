# BasketballPlayerApi

This API provides league commissioners, coaches, and statistics analysts with a highly performant, type-safe backend platform. 
* **In-Scope Features:** Layered CRUD management for professional rosters, structured game logging, realistic box-score tracking (Points, Rebounds, Assists), automated team leaderboard calculations, and pagination/filtering optimizations for wide dataset pools.
* **Deferred Features:** Future iterations will support real-time live match updates using SignalR/WebSockets and bracket generation engines.

---

## 📊 Database Schema & Relationship Layout

The architecture structures database persistence through **4 highly connected domain models** to fulfill core project constraints—incorporating explicit primary/foreign keys, navigation collections, and cascading constraints.

### Relationships Summary
* **Team ➡️ Player (1:N):** A single team consists of multiple players. If a team entry changes, individual player associations update safely.
* **Game ➡️ PlayerGameStat (1:N):** A recorded game maps downstream to multiple individual box scores.
* **Player ➡️ Game (M:N):** Connected through the explicit bridge/join entity **`PlayerGameStat`**. This accommodates unique attributes tracking *Points*, *Rebounds*, and *Assists* mapped to a specific athlete inside a specific match.

---

## 📁 Technical Architecture & File Structure

The workspace follows strict separation of concerns across multiple layered project assemblies:

* **`BasketballPlayerApi` (Presentation / API Layer):** Exposes thin controllers (`PlayersController`, `StatsController`) to parse REST query strings, enforce route payloads, and hand off operational loops to the business tier.
* **`BasketballPlayerApi.BLL` (Business Logic Layer):** Houses the transactional service contracts (`LeagueService`), dedicated Input/Output DTO definitions, and data translation pipelines. Direct database tables are isolated from consumers using safe manual object translation mappings.
* **`BasketballPlayerApi.DAL` (Data Access Layer):** Hosts the Entity Framework infrastructure, entity configurations, structural metadata loops (`LeagueDbContext`), and persistence hooks.
* **`BasketballPlayerApi.Models` (Domain Objects Layer):** Centralizes the clean tracking schemas and relational object properties shared between data wrappers and database mappings.

---

## 🔒 Authentication Credentials

To explore or interact with protected management features on an active environment deployment interface, inject the administrative client profile data below inside your security authorization wrapper blocks:

* **Authorization Role:** `League Administrator`
* **Default Client App ID:** `basketball-analytics-client-v1`
* **Access Scope Signature:** `api://basketball-league/admin.write`
* **Local Development Environment Gateway Bypass:** Active (By default, the platform uses local developer exception configurations and an internal safe mock storage context for verification checks).

---
### Quick Start Instructions
1. Open a terminal path at the project root folder.
2. Build the solution environment configurations:
   ```bash
   dotnet build
   ```
3. Run the API Presentation host application layer:
   ```bash
   dotnet run --project BasketballPlayerApi/BasketballPlayerApi.csproj
   ```
4. Access the polished **Swagger UI and Interactive OpenAPI Documentation** portal by navigating to the address matching your runtime host mapping environment:
   ```url
   http://localhost:5000/swagger
   ```

### Operational API Standard Status Codes
* `200 OK` - Collection fetching or analytic lookup succeeded.
* `201 Created` - Resource generation sequence passed (Returns target location header).
* `204 No Content` - State mutation or deletion finished smoothly.
* `400 Bad Request` - Data validation filters or business validation bounds failed.
* `404 Not Found` - Requested resource identifier could not be matched.
* `500 Internal Server Error` - Caught gracefully and converted into an RFC 7807 problem details response model.
