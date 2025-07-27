# Task Tracker Application

A simple task management application built with Clean Architecture principles.

## Architecture Overview

The solution is structured into four layers:

1. **Domain** - Contains core business models and repository interfaces
   - `TaskItem` entity with business rules
   - `ITaskRepository` interface

2. **Application** - Contains business logic and service interfaces
   - `ITaskService` interface
   - `TaskService` implementation with business rules

3. **Infrastructure** - Contains implementation details
   - `InMemoryTaskRepository` implementation
   - Dependency Injection configuration

4. **ConsoleApp** - User interface layer
   - Menu-driven console interface
   - No business logic, only presentation

## Dependencies

- .NET 8.0
- Microsoft.Extensions.DependencyInjection (for DI)
- Moq (for unit testing)
- xUnit (for unit testing)

## How to Run

1. Navigate to the `TaskTracker.ConsoleApp` project directory
2. Run `dotnet run`

## Features

- View all tasks
- Add new tasks
- Mark tasks as complete
- Simple in-memory storage

## Unit Testing

Run tests with:
# Run all tests
dotnet test

# Run specific test project
dotnet test TaskTracker.Domain.Tests
dotnet test TaskTracker.Application.Tests
