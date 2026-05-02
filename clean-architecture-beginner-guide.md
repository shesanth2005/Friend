# Clean Architecture Folder Structure — A Beginner-Friendly Guide

## What Clean Architecture Is (Without the Jargon)

**Clean Architecture** is a way to organize code so that *the important business rules stay in the center*, and *details like databases, web frameworks, and UI sit on the outside*.

Think of it as **drawing circles**:

- **Inner rings** = stable ideas that rarely change (“what the system *is supposed to do*”).
- **Outer rings** = things that often change (“*how* we talk to users, disks, APIs”).

Dependency direction is strict: **outer layers depend on inner layers—not the reverse.**  
The inner core does **not** know whether you use PostgreSQL or MongoDB, or whether users click buttons in a browser or tap a mobile app.

---

## A Simple Real-World Analogy: A Restaurant Kitchen

Imagine a restaurant:

| Clean Architecture concept | Restaurant analogy |
|----------------------------|--------------------|
| **Domain** | Recipes and food standards — *what counts as good food*, allergen rules, portion logic. Independent of stoves or deliveries. |
| **Application / use cases** | The *service flow*: “Guest orders pasta → chef cooks → waiter serves.” Procedures and workflows. |
| **Infrastructure** | Suppliers, refrigerators, stoves, POS machine — concrete tools that fulfill the rules. Swap the supplier without rewriting the recipes. |
| **Presentation / API / UI** | The dining room and menu — how guests interact. You could switch from paper menus to a tablet app without changing how the chef thinks. |

Your **recipe book (domain)** shouldn’t mention “Samsung fridge.”  
Your **fridge (infrastructure)** implements storage; it follows what the kitchen *needs*, not the other way around.

---

## Why People Use This

1. **Change is cheaper.** Switch database, hosting, or UI framework without rewriting business rules.
2. **Easier testing.** Pure rules and workflows can be tested without spinning up HTTP or SQL.
3. **Clear boundaries.** Teams know where “policy” ends and “plumbing” begins.
4. **Less spaghetti.** Features don’t secretly reach across the whole codebase.

---

## Typical Folder / Project Layout

Names vary (“Core,” “Shared,” “Web”), but **the idea is the same**: dependencies point **inward**.

---

### Folder trees (quick visual)

Trees like these are **conventions**, not scripture: only create folders when you have code to put in them. Read from top to bottom: **solution → projects → folders**.

#### Generic Clean Architecture layout (reference)

```
YourSolution/
├── YourApp.Domain/                 ← inner ring: pure business concepts
│   ├── Entities/
│   ├── ValueObjects/
│   ├── Events/
│   └── Exceptions/
│
├── YourApp.Application/           ← use cases + “ports” (interfaces)
│   ├── Interfaces/                ← e.g. IOrderRepository, IEmailSender
│   ├── Services/                  ← or Handlers/, depending on style
│   ├── Dtos/
│   └── Features/                  ← optional: organize by feature/vertical slice
│       └── Orders/
│           ├── Commands/
│           ├── Queries/
│           └── Dtos/
│
├── YourApp.Infrastructure/        ← adapters: databases, SMS, HTTP clients
│   ├── Persistence/
│   │   ├── Configurations/
│   │   ├── Migrations/
│   │   └── Repositories/
│   ├── ExternalServices/
│   └── Messaging/
│
├── YourApp.API/                   ← outer ring: HTTP, DI composition root often here
│   ├── Controllers/
│   ├── Middleware/
│   ├── Filters/
│   ├── Models/
│   │   ├── Requests/
│   │   └── Responses/
│   ├── Mappings/
│   ├── Extensions/
│   └── Program.cs
│
└── YourApp.sln
```

#### This repo’s shape (Friend — simplified)

Your solution follows the same **Domain → Application → Infrastructure → API** idea. Below is the important **source layout** (build output and `obj/`/`bin/` omitted).

```
Friend/
├── Friend.Domain/
│   └── (your domain types and rules live here)
│
├── Friend.Application/
│   └── Features/
│       └── Accounts/
│           ├── Commands/
│           │   ├── LoginUser/
│           │   └── RegisterUser/
│           ├── Queries/
│           │   └── GetUserById/
│           └── Dtos/
│
├── Friend.Infrastructure/
│   └── Persistence/
│       ├── AppDbContext.cs
│       └── Migrations/
│
├── Friend.API/
│   ├── Controllers/
│   ├── Extensions/
│   ├── Mappings/
│   └── Models/
│       ├── Requests/
│       └── Responses/
│
├── Friend.slnx                    ← solution file (name may vary)
└── clean-architecture-beginner-guide.md
```

**How to read the arrows in your head:** `Friend.API` and `Friend.Infrastructure` sit on the **outside**; they **depend on** `Friend.Application` and `Friend.Domain`. `Friend.Domain` does **not** depend on the API or EF Core.

---

### 1. Domain (center)

**What:** Entities, value objects, domain events, pure business rules, maybe domain interfaces (ports).

**Analog:** The recipe standards — no mention of ovens or URLs.

Typical folders:

- `Entities` — nouns your business cares about (e.g., `Order`, `Customer`).
- `ValueObjects` — small immutable concepts (money, dates with rules).
- `Events` — “something meaningful happened.”
- `Exceptions` — domain-specific errors.

**Rule of thumb:** No references to EF Core, ASP.NET, HTTP, or SMTP here.

---

### 2. Application (use cases)

**What:** Application services, commands/queries (CQRS), DTOs, interfaces that *the application* needs but does not implement (e.g., `IOrderRepository`), validation of workflows.

**Analog:** Waiter flows and chef steps — orchestration, not the gas line.

Typical folders:

- `Interfaces` / `Abstractions` — ports (e.g., `IBillingGateway`).
- `Services` — use cases composing domain + ports.
- `DTOs` / `Models` — data shapes for crossing boundaries between layers.

**Depends on:** Domain only (and shared primitives).

---

### 3. Infrastructure (implementations)

**What:** Implements interfaces from Application or Domain — database access, file storage, email, external HTTP APIs, EF Core mappings, Redis, etc.

**Analog:** Trucks, refrigerators, payment terminals — swap them; recipes stay valid.

Typical folders:

- `Persistence` — DbContext, migrations, repository implementations.
- `ExternalServices` — third-party API clients.
- `Messaging` — queues, event bus adapters.

**Depends on:** Application (and Domain through those contracts).

---

### 4. Presentation / API / UI (outer shell)

**What:** Controllers, minimal APIs, GraphQL, Blazor, MVC views, mobile clients — anything that talks to the outside world.

**Analog:** The dining room — how the guest orders; the kitchen doesn’t depend on the menu design.

Typical folders:

- `Controllers` — HTTP endpoints.
- `Middleware`, `Filters` — cross-cutting web concerns.
- Maybe `ViewModels` if you keep UI-specific shapes here.

**Depends on:** Application (and sometimes Infrastructure for DI wiring only — many teams register implementations in the composition root only).

---

## How This Maps to a .NET Solution (Like Yours)

A common pattern:

| Project / folder   | Role        |
|--------------------|-------------|
| `YourApp.Domain`  | Entities, domain logic, minimal interfaces owned by domain. |
| `YourApp.Application` | Use cases, DTOs, application ports (`I*` interfaces). |
| `YourApp.Infrastructure` | EF Core, repos, SMTP, integrations. |
| `YourApp.API` (or `.Web`) | Controllers/Program.cs, Startup wiring. |

`*API*` references `Application` + `Infrastructure` mainly for dependency injection (“plug in implementations”), while **references between Domain ↔ Application ↔ Infrastructure** flow **toward Domain**, not away from it.

---

## Mental Model Checklist for Beginners

- **Ask:** “If we replaced the database tomorrow, would I need to edit this folder?”  
  If yes — it belongs in **Infrastructure** (or the outer rim), not **Domain**.
- **Ask:** “Is this describing *business truth*?” → lean toward **Domain**.
- **Ask:** “Is this *orchestrating one user goal*?” → lean toward **Application**.
- **Ask:** “Is this *HTTP, SQL, SMTP, filesystem*?” → **Infrastructure** or **Presentation**.

---

## One Caveat

“Clean Architecture” is a **design idea**, not a single official folder tree. Teams rename projects and split folders; what matters is **dependency direction** and **separating policy from details**. Use this guide as a map, then adapt names to your team’s conventions.

---

*Document purpose: introductory overview of Clean Architecture folder structure using everyday analogies. For deeper theory, Robert C. Martin’s original writing on Clean Architecture expands on the dependency rule and boundaries.*
