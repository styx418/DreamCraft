# DreamCraft

This repository provides a minimal example of a modular TCP/UDP server written in C#. Each server component is implemented as a separate module implementing the `IServerModule` interface. All log messages are colorized for clarity.

## Building

A .NET SDK is required to build and run the server. Restore packages and build
the project using the provided `DreamCraft.csproj` file:

```bash
dotnet restore
dotnet build DreamCraft.csproj
dotnet run --project DreamCraft.csproj
```

## Modules

- **TcpGameServer** — handles TCP connections.
- **UdpGameServer** — handles UDP packets.
- **Logger** — outputs colored messages (info, warnings, and errors).
- **StartupBanner** — displays an ASCII art banner when the server starts.
- **ModuleManager** — loads and initializes all modules at startup.

The `Program` class loads modules via `ModuleManager` and starts each one.
