# Taskify

Author: Tino Dragojević

---

## Overview

Taskify is an application designed to streamline task management within teams. The idea is to create boards where individuals can assign tasks to team members, who then complete them. The concept is similar to project boards seen on platforms like GitHub.

---

## Technologies Used

- ASP.NET
- Entity Framework Core (EF Core)
- JwtBearer
- FluentValidation
- Riok.Mapperly
- System.IdentityModel.Tokens.Jwt
- SQL Server

---

## Purpose

The purpose of Taskify is to provide teams with a user-friendly platform for organizing and managing tasks effectively. By utilizing features such as boards and task assignments, teams can collaborate efficiently and track progress on various projects.

---

## Installation

To run Taskify locally, follow these steps:

1. Clone the repository: `git clone https://github.com/your-username/taskify.git`
2. Navigate to the project directory: `cd taskify`
3. Install dependencies: `dotnet restore`
4. Configure the database connection in `appsettings.json`
5. Apply EF Core migrations: `dotnet ef database update`
6. Run the application: `dotnet run`

---

