# Hiking Companion

A web application for managing hiking equipment, creating packing lists, and tracking total pack weight.

## Features

- Manage hiking equipment and categories through a REST API.
- Create and manage packing lists.
- Basic frontend for displaying equipment.
- More features to be added.

## Tech Stack

- **Backend:** C# / ASP.NET Core Minimal API
- **Frontend:** HTML, CSS, JavaScript
- **ORM:** Entity Framework Core
- **Database:** PostgreSQL 18
- **Containerization:** Docker Compose
- **API Documentation:** OpenAPI / Scalar

## Getting Started

### Prerequisites

- .NET SDK
- Docker and Docker Compose
- VS Code with the Five Server extension (or another local web server)

### Running the Application

**1. Start PostgreSQL**

From the project root:

```bash
docker compose up -d
```

**2. Start the backend**

From `gearApi/`:

```bash
dotnet watch run
```

The API runs at `http://localhost:5289` when using the configured development port.

**3. Start the frontend**

1. Open `frontend/index.html` in VS Code.
2. Right-click and select **Open with Five Server**.
3. The frontend opens in your browser, typically at `http://127.0.0.1:5500`.

The frontend uses JavaScript `fetch()` to communicate with the backend API.

**Note:** The backend uses CORS to allow requests from the frontend. If Five Server uses a different host or port, update the allowed origin in `gearApi/Program.cs`.

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