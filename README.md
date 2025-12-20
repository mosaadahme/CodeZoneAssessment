# 🎓 Course Enrollment System

A high-quality ASP.NET Core web application for managing students, courses, and enrollments. This project demonstrates **Clean Architecture** principles, **SOLID design**, and modern web development practices.

## 🚀 Overview
The system provides a robust solution for university administrators to manage student records and course registrations. It handles complex business logic such as capacity validation and duplicate prevention while maintaining a seamless user experience.

## 🛠️ Tech Stack & Patterns
* **Framework:** .NET 8 (ASP.NET Core MVC)
* **Database:** Entity Framework Core (In-Memory Provider for easy portability)
* **Architecture:** N-Tier / Clean Architecture (Core, Data, Services, Web)
* **Design Patterns:** Repository Pattern, Unit of Work, Dependency Injection (DI)
* **Validation:** FluentValidation (for decoupled validation logic)
* **Mapping:** AutoMapper (for DTO/Entity transformations)
* **Testing:** xUnit & Moq (for unit testing business logic)
* **Frontend:** Bootstrap 5, jQuery, AJAX, and Bootstrap Icons

## ✨ Key Features
* **N-Tier Architecture:** Complete separation of concerns across four distinct layers (Core, Data, Services, Web).
* **Smart Enrollment Logic:** * Real-time capacity checks to prevent over-enrollment.
    * Duplicate enrollment prevention for the same student in the same course.
* **Advanced UI Features:** * **AJAX Modals:** View a student's enrolled courses or a course's registered students instantly via popup modals without page reloads.
    * **Server-side Search & Pagination:** Optimized performance for large datasets using efficient filtering.
* **Global Exception Handling:** Custom Middleware to catch and log errors globally, ensuring consistent API/UI responses and system stability.
* **Data Seeding:** Automatically initializes the system with sample data for immediate testing upon startup.

## 🧪 Unit Testing & Quality Assurance
The project includes a dedicated testing suite to ensure reliability:
* **Business Logic Testing:** Unit tests for the `EnrollmentService` covering both "Success" and "Failure" (e.g., full capacity) scenarios.
* **Mocking:** Utilizes **Moq** to isolate service logic from database dependencies for accurate testing.

## 🐳 Docker Support
The project is fully containerized and ready for deployment:
* **Multi-stage Build:** Optimized Dockerfile using separate build and runtime stages to minimize image size.
* **Run with Docker:** ```bash
  docker build -t codezone-app -f CodeZone.Web/Dockerfile .