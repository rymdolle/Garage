# Garage

Simple console application to park, list and search vehicles in a fixed-capacity garage. Built in C# targeting .NET 10.

## Features
- Park and remove vehicles (`Car`, `Motorcycle`, `Bus`, `Airplane`, `Boat`)
- Search and filter vehicles by attributes (`color`, `type`, `regnr`, `make`, `model`)
- List vehicles and counts by type
- Optional demo data population

## Requirements
- .NET 10 SDK
- Visual Studio 2026 (or any editor that supports .NET 10)
- C# language version 14.0

## Quick start

1. Open the solution in Visual Studio 2026 and build.
2. Or use the .NET CLI:
   - Restore & build:
     ```
     dotnet build
     ```
   - Run:
     ```
     dotnet run --project Garage
     ```

When the app starts it will ask for a garage capacity and (optionally) whether to populate demo vehicles.

## Configuration
The app reads configuration values via `Microsoft.Extensions.Configuration`. Example settings:
- `demo` (boolean) — default `false`. If `true` the app will offer to populate demo vehicles on startup.

Place configuration in `appsettings.json`.

## Project layout (important files)
- `Garage/` — core domain and application code
  - `Garage.cs` — generic `Garage<T>` storage implementation
  - `Manager.cs` — application flow, menus and user interactions
- `Garage/UserInterface/`
  - `IUserInterface.cs` — abstraction used by the console UI and tests
  - `Menu.cs` — menu model used by `Manager`
- `Garage/Vehicles/` — vehicle types and base `Vehicle` class
- `LICENSE` — project license (MIT)

## Usage notes
- Registration numbers must be unique. The app enforces uniqueness and capacity limits.
- `Manager` uses the configuration key `demo` to control demo population.
- The UI is abstracted via `IUserInterface` to allow easy testing or substitution.

## License
This project is released under the MIT License. See `LICENSE` for details.
