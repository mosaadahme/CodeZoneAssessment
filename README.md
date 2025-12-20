# 🎓 Course Enrollment System

A robust ASP.NET Core web application for managing students, courses, and enrollments. Built using **Clean Architecture** principles and **Best Practices** to ensure scalability and maintainability.

## 🚀 Overview
This system allows administrators to manage university records and handles the complex logic of student enrollments, ensuring data integrity and validation (e.g., course capacity checks, duplicate prevention).

## 🛠️ Tech Stack & Patterns
* **Framework:** .NET 8 (ASP.NET Core MVC)
* **Database:** Entity Framework Core (In-Memory Provider)
* **Architecture:** N-Tier / Clean Architecture (Core, Data, Services, Web)
* **Design Patterns:** Repository Pattern, Unit of Work, Dependency Injection (DI)
* **Validation:** FluentValidation
* **Mapping:** AutoMapper
* **Testing:** xUnit & Moq
* **Frontend:**  jQuery, AJAX

## ✨ Key Features
* **Structured Architecture:** Separation of concerns using Core, Data, and Service layers.
* **CRUD Operations:** Full management for Students and Courses.
* **Smart Enrollment:** * Prevents duplicate enrollments.
    * Validates course capacity before accepting students.
    * **AJAX-based UI** for seamless, real-time updates without page reloads.
* **Search & Pagination:** Optimized performance for large datasets using server-side filtering.
* **Seed Data:** The system automatically initializes with sample data upon startup.

## 🧪 Unit Testing
The project includes unit tests for the critical **Business Logic** (Enrollment Service) using **xUnit** and **Moq** to ensure reliability.
* **Test Coverage:** Validates successful enrollments and error handling (e.g., full courses).

## ⚙️ How to Run
1.  Clone the repository.
2.  Open `CodeZone.sln` in **Visual Studio 2026**.
3.  Set `CodeZone.Web` as the Startup Project.
4.  Run the application (F5).
    * *Note: No SQL Server setup is required. The project uses an In-Memory database for demonstration purposes.*

---
*Developed with by Mosaad Ahmed*