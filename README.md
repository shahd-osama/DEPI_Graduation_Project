# TEFLY  Centralized Pediatric Health & Vaccination Platform

![.NET](https://img.shields.io/badge/.NET_10-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core_10_MVC-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=flat-square&logo=microsoftsqlserver&logoColor=white)
![EF Core](https://img.shields.io/badge/EF_Core_10-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![DEPI](https://img.shields.io/badge/DEPI-Graduation_Project-001F3F?style=flat-square)
![Vision 2030](https://img.shields.io/badge/Egypt_Vision-2030-AEC6CF?style=flat-square)
![WHO](https://img.shields.io/badge/WHO-EPI_Compliant-0093D5?style=flat-square)

---

**TEFLY** (Arabic:"My Child") is a centralized pediatric health and vaccination tracking platform developed under Egypt's **Digital Egypt Pioneers Initiative (DEPI)**. The platform replaces Egypt's fragmented, paper-based vaccination tracking system with a unified digital solution that helps parents monitor their children's full immunization history.

> **Project Name:** VaccinationTracker ” TEFLY  
> **Version:** v1.0.0  
> **Team:** PentApex ” Egypt  
> **Initiative:** DEPI ” Egypt Vision 2030, Digital Healthcare Pillar  
> **Classification:** DEPI Graduation Submission

---

## Table of Contents

- [Problem Statement](#problem-statement)
- [Platform Modules](#platform-modules)
- [Technology Stack](#technology-stack)
- [System Architecture](#system-architecture)
- [Architectural Decisions](#architectural-decisions)
- [Database Design](#database-design)
- [Security](#security)
- [UI Modules](#ui-modules)
- [Design System](#design-system)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Standards and Compliance](#standards-and-compliance)
- [SDG Alignment](#sdg-alignment)
- [Team](#team)
- [License](#license)

---

## Problem Statement

Egypt's existing child vaccination system relies on paper-based records that are siloed across clinics and governorates. There is no unified registry that tracks immunization history for a child across the full national healthcare network. This leads to:

- Missed or duplicate vaccinations due to lost or inaccessible records.
- No centralized visibility for the Ministry of Health (MOH) on herd immunity coverage.
- No automated reminder infrastructure for parents.
- Inability to scale monitoring or reporting across 27 governorates.

TEFLY addresses each of these gaps with a WHO EPI-compliant platform built on a clean, maintainable layered architecture.

---

## Platform Modules

| Module | Description |
| :--- | :--- |
| **Authentication & Authorization** | Role-based access control with ASP.NET Core Identity (Admin, User). |
| **Children Management** | Full CRUD for child profiles linked to parent accounts. |
| **Vaccination Records** | Per-child immunization history tracking aligned with the WHO EPI Egypt schedule (Birth â†’ 18 months). |
| **Appointment Booking** | Appointment creation and management per child, with upcoming appointment visibility on the dashboard. |
| **Adverse Reactions** | Logging of post-vaccination side effects per child and per vaccine. |
| **Vaccine Side Effects** | Reference data management for known vaccine side effects. |
| **Complaints** | User complaint submission with Admin review and management. |
| **Health Awareness** | Admin-managed health articles (Published / Draft) visible to all users. |
| **Admin Panel** | Dedicated Admin area for full management of all platform entities. |

---

## Technology Stack

```
Framework        ASP.NET Core 10 MVC
Language         C#
Database         SQL Server
ORM              Entity Framework Core 10
Mapping          AutoMapper
Auth             ASP.NET Core Identity (Cookie-based, Role-based)
Architecture     3-Layer (Presentation / BLL / DAL)
Patterns         Repository, Unit of Work, Service Layer
Frontend         Tailwind CSS, Razor Views, Lucide Icons, Google Fonts (Cairo)
IDE              Microsoft Visual Studio
```

---

## System Architecture

TEFLY follows a strictly enforced **3-Layer Architecture** implemented as separate projects within one solution:

```
+--------------------------------------------------------------+
|                    PRESENTATION LAYER                        |
|    ASP.NET Core 10 MVC â€” Controllers, Razor Views, Areas     |
+--------------------------------------------------------------+
                             |
+--------------------------------------------------------------+
|              BUSINESS LOGIC LAYER (BLL)                      |
|    Services  |  DTOs  |  AutoMapper Profiles  |  Interfaces  |
+--------------------------------------------------------------+
                             |
+--------------------------------------------------------------+
|              DATA ACCESS LAYER (DAL)                         |
|    EF Core 10  |  SQL Server  |  Repositories  |  UnitOfWork |
+--------------------------------------------------------------+
```

### Role-Based Routing

- **Admin** ’ redirected to the `Admin` Area with full management access.
- **User (Parent)** ’ accesses the user-facing interface: children, records, appointments, awareness.

### Request Flow (Example: Create Appointment)

1. Anti-Forgery token validation and model binding in the Controller.
2. Controller delegates to `IAppointmentService` via constructor injection.
3. Service applies business rules and maps ViewModel ’ DTO ’ Entity.
4. Repository persists the entity via `IUnitOfWork`.
5. Controller returns redirect with `TempData` success message.

---

## Architectural Decisions

| ADR | Decision | Rationale |
| :--- | :--- | :--- |
| **ADR-001** | 3-Layer MVC over Clean Architecture | Reduces delivery risk for a 5-member team within the DEPI timeline while preserving a clear, auditable separation of concerns. |
| **ADR-002** | Repository + Unit of Work | Provides a consistent data access abstraction, simplifies testing, and centralizes transaction management. |
| **ADR-003** | AutoMapper for all layer boundaries | Enforces strict ViewModel ” DTO ” Entity separation; prevents domain model leakage into the Presentation layer. |
| **ADR-004** | ASP.NET Core Identity (cookie-based) | Sufficient for an MVC application with server-rendered views; avoids unnecessary JWT complexity for this use case. |
| **ADR-005** | Separate BLL and DAL projects | Physical project separation enforces layer boundaries at compile time ” not just by convention. |

---

## Database Design

- **Primary Keys:** `int` identity columns.
- **Seeding:** `DbSeeder` seeds Vaccines (13 vaccines, WHO EPI Egypt schedule) and Awareness articles on first run.
- **Inheritance:** TPH (`Discriminator` column) used where applicable.

### Core Entities

| Entity | Key Relations |
| :--- | :--- |
| `ApplicationUser` | Extends `IdentityUser`; linked to Children |
| `Child` | Belongs to a User; has VaccinationRecords, Appointments, AdverseReactions |
| `Vaccine` | Has VaccinationSchedules, VaccinationRecords, VaccineSideEffects |
| `VaccinationSchedule` | Links Vaccine ’ AgeStage ’ DoseNumber |
| `VaccinationRecord` | Records a completed vaccination for a Child |
| `Appointment` | Booking for a Child's upcoming vaccination |
| `AdverseReaction` | Post-vaccination reaction linked to Child and Vaccine |
| `Complaint` | User-submitted complaint for Admin review |
| `Awareness` | Health article with Status (Published / Draft) and Category |

### Vaccination Schedule " WHO EPI Egypt

| Vaccine | Age Stage | Dose |
| :--- | :--- | :--- |
| BCG | Birth | 1 |
| Hepatitis B | Birth | 1 |
| Polio (OPV) | Birth | 1 |
| Pentavalent 1 | 2 months | 1 |
| Polio IPV/OPV | 2 months | 2 |
| Pentavalent 2 | 4 months | 2 |
| Polio IPV/OPV | 4 months | 3 |
| Pentavalent 3 | 6 months | 3 |
| Polio IPV/OPV | 6 months | 4 |
| Polio Booster | 9 months | 5 |
| MMR | 12 months | 1 |
| MMR Booster | 18 months | 2 |
| DTP Booster | 18 months | 4 |

---

## Security

| Layer | Implementation |
| :--- | :--- |
| Authentication | ASP.NET Core Identity ” cookie-based |
| Authorization | Role-based: `[Authorize(Roles = "Admin")]` / `[Authorize]` |
| Admin Isolation | Dedicated `Admin` Area, inaccessible to regular users |
| Input Validation | Anti-Forgery tokens on all POST actions + server-side `ModelState` validation |
| Data Access | All queries go through the Service ’ Repository ’ UnitOfWork chain |

---

## UI Modules

| View | Path | Status |
| :--- | :--- | :--- |
| Shared Layout | `Views/Shared/_Layout.cshtml` |  Complete |
| Home / Welcome | `Views/Home/` |  Complete |
| User Dashboard | `Views/Home/Dashboard.cshtml` |  Complete |
| Children | `Views/Child/` |  Complete |
| Vaccination Records | `Views/VaccinationRecord/` |  Complete |
| Appointments | `Views/Appointment/` |  Complete |
| Adverse Reactions | `Views/AdverseReaction/` |  Complete |
| Vaccine Side Effects | `Views/VaccineSideEffect/` |  Complete |
| Complaints | `Views/Complaint/` |  Complete |
| Health Awareness (User) | `Views/Awareness/` |  Complete |
| Admin â€” All Entities | `Areas/Admin/Views/` |  Complete |

---

## Design System

### Color Palette

| Token | Hex | Usage |
| :--- | :--- | :--- |
| `--navy` | `#001F3F` | Primary backgrounds, headings, navigation |
| `--turquoise` | `#40E0D0` | Accent, CTA buttons, active states |
| `--baby-blue` | `#AEC6CF` | Secondary surfaces, KPI card accents |
| `--baby-pink` | `#F4C2C2` | Status indicators, soft highlights |
| `--surface` | `#F8FAFC` | Page background |

### Typography

- **Primary Font:** Cairo (Google Fonts) ” supports full Arabic/English bilingual rendering.
- **Weights Used:** 300, 400, 500, 600, 700, 800, 900.
- **Direction Support:** RTL (Arabic) and LTR (English) via `dir` attribute on `<html>`.

### UI Principles

- **Glassmorphism** navbar: `backdrop-filter: blur(18px) saturate(1.6)` with semi-transparent Navy background.
- **Soft UI** card system (`glass-card`): layered box shadows, pastel surfaces, rounded corners.
- **Status Badges:** `Published` (green), `Draft` (slate).
- **Animations:** Spring easing `cubic-bezier(0.16, 1, 0.3, 1)` for all transitions.

---

## Getting Started

### Prerequisites

- .NET 10 SDK
- SQL Server (local or Azure SQL)
- Microsoft Visual Studio 2022+

### Installation

```bash
# Clone the repository
git clone https://github.com/shahd-osama/DEPIGraduationProject.git
cd DEPIGraduationProject

# Restore .NET dependencies
dotnet restore

# Apply database migrations
dotnet ef database update --project VaccinationTracker.DAL --startup-project VaccinationTracker

# Run the application
dotnet run --project VaccinationTracker
```

The `DbSeeder` runs automatically on first startup and seeds:
- Admin and default User accounts
- 13 vaccines aligned with the WHO EPI Egypt schedule
- Vaccination schedules (Birth 18 months)
- Sample Health Awareness articles

### Configuration

Update `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=VaccinationTrackerDb;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

---

## Project Structure

```
VaccinationTracker/               â† Solution root
â”‚
â”œâ”€â”€ VaccinationTracker.sln
â”‚
â”œâ”€â”€ VaccinationTracker.DAL/       â† Data Access Layer
â”‚   â”œâ”€â”€ Models/                   â† Domain entities
â”‚   â”œâ”€â”€ Data/
â”‚   â”‚   â”œâ”€â”€ VaccinationContext.cs
â”‚   â”‚   â”œâ”€â”€ DbSeeder.cs
â”‚   â”‚   â””â”€â”€ Migrations/
â”‚   â”œâ”€â”€ Repositories/             â† Generic + specific repositories
â”‚   â””â”€â”€ UnitOfWork/
â”‚
â”œâ”€â”€ VaccinationTracker.BLL/       â† Business Logic Layer
â”‚   â”œâ”€â”€ DTOs/
â”‚   â”œâ”€â”€ Services/
â”‚   â”‚   â”œâ”€â”€ Interfaces/
â”‚   â”‚   â””â”€â”€ Implementations/
â”‚   â””â”€â”€ Mappings/                 â† AutoMapper profiles (BLL DTOs)
â”‚
â””â”€â”€ VaccinationTracker/           â† Presentation Layer (MVC)
    â”œâ”€â”€ Controllers/
    â”‚   â”œâ”€â”€ HomeController.cs
    â”‚   â”œâ”€â”€ ChildController.cs
    â”‚   â”œâ”€â”€ VaccinationRecordController.cs
    â”‚   â”œâ”€â”€ AppointmentController.cs
    â”‚   â”œâ”€â”€ AdverseReactionController.cs
    â”‚   â”œâ”€â”€ VaccineSideEffectController.cs
    â”‚   â”œâ”€â”€ ComplaintController.cs
    â”‚   â””â”€â”€ AwarenessController.cs
    â”œâ”€â”€ Areas/
    â”‚   â””â”€â”€ Admin/
    â”‚       â”œâ”€â”€ Controllers/
    â”‚       â”‚   â”œâ”€â”€ ChildrenController.cs
    â”‚       â”‚   â”œâ”€â”€ VaccinationRecordsController.cs
    â”‚       â”‚   â”œâ”€â”€ AppointmentsController.cs
    â”‚       â”‚   â”œâ”€â”€ AdverseReactionsController.cs (Admin)
    â”‚       â”‚   â”œâ”€â”€ VaccineSideEffectsController.cs
    â”‚       â”‚   â”œâ”€â”€ ComplaintsController.cs
    â”‚       â”‚   â””â”€â”€ AwarenesssController.cs
    â”‚       â””â”€â”€ Views/
    â”œâ”€â”€ ViewModels/
    â”œâ”€â”€ Mappings/                 â† AutoMapper profiles (ViewModels)
    â”œâ”€â”€ Views/
    â””â”€â”€ wwwroot/
```

---

## Standards and Compliance

| Standard | Scope |
| :--- | :--- |
| WHO Expanded Programme on Immunization (EPI) Egypt | Vaccination schedule (13 vaccines, Birth  18 months) |
| OWASP Top 10 | Anti-Forgery tokens, server-side validation, role-based authorization |
| Egypt Vision 2030  Digital Healthcare Pillar | Strategic alignment |

---

## SDG Alignment

| Goal | Alignment |
| :--- | :--- |
| **SDG 3  Good Health and Well-Being** | Lifetime vaccination tracking per child, adverse reaction reporting, and health awareness content. |
| **SDG 9  Industry, Innovation, and Infrastructure** | Replacing paper-based systems with a scalable digital platform for national healthcare infrastructure. |
| **SDG 17  Partnerships for the Goals** | Designed for Ministry of Health integration and WHO schedule compliance. |

---

## Team

**PentApex**  Egypt

Developed under the **Digital Egypt Pioneers Initiative (DEPI)** as a graduation project aligned with Egypt Vision 2030.

- GitHub: [DEPIGraduationProject](https://github.com/shahd-osama/DEPIGraduationProject.git)
- Initiative: [depi.gov.eg](https://depi.gov.eg)
- Vision 2030: [vision2030.gov.eg](https://vision2030.gov.eg)

---

## License

**RESTRICTED / CONFIDENTIAL**

This repository and all its contents are the intellectual property of the PentApex development team, submitted as a graduation project under the DEPI initiative. Unauthorized reproduction, distribution, or commercial use is strictly prohibited.

&copy; 2026 PentApex  All rights reserved.

---

*TEFLY  Protecting Egypt's children, one vaccination at a time.*
