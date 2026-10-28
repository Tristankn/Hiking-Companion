# Hiking Companion

A web application for managing hiking equipment, creating packing lists, and tracking total pack weight.

## Features

*To be added.*

## Tech Stack

- **Backend:** C# / ASP.NET Core Minimal API
- **ORM:** Entity Framework Core
- **Database:** PostgreSQL 18
- **Containerization:** Docker Compose
- **API Documentation:** OpenAPI / Scalar

## Getting Started

### Prerequisites

*To be added.*

### Installation

*To be added.*

### Running the Application

*To be added.*

## Database

### Docker Setup

Start PostgreSQL from the project root:

```bash
docker compose up -d
```

Check container status:

```bash
docker compose ps
```

### Database Migrations

When changing EF Core data models that affect the database schema, create a new migration.

Run from the backend project directory (`gearApi/`):

```bash
# Create a migration
dotnet ef migrations add MigrationName

# Apply pending migrations
dotnet ef database update
```

Replace `MigrationName` with a descriptive name, e.g. `AddGearDescription`.

### Inspect Database Tables

Run from the project root to list PostgreSQL tables:

```bash
docker compose exec postgres psql -U postgres -d hiking_companion -c "\dt"
```

### Test Database Persistence

1. Start the API:

   ```bash
   dotnet watch run
   ```

2. Create equipment using `POST /gearlist`.
3. Verify the equipment exists using `GET /gearlist`.
4. Stop and restart the API.
5. Run `GET /gearlist` again and verify the equipment still exists.

To additionally verify persistence across a database restart, run from the project root:

```bash
docker compose restart postgres
```

Then repeat the GET request after PostgreSQL is ready.

Data should persist across application and container restarts.