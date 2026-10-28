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

Start the PostgreSQL container from the project root:

```bash
docker compose up -d
```

Stop the container:

```bash
docker compose stop
```

### Database Migrations

When changing EF Core data models that affect the database schema, create a new migration.

Run from the backend project directory:

```bash
# Create a migration
dotnet ef migrations add MigrationName

# Apply pending migrations
dotnet ef database update
```

Replace `MigrationName` with a descriptive name, e.g. `AddGearDescription`.

Ensure PostgreSQL is running before applying migrations.

## API Endpoints

*To be documented.*

## Project Structure

*To be documented.*

## Future Development

*To be added.*