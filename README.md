# Overview
Inventory-o. Your inventory, easier.
Inventory management console app based on C# and the .NET ecosystem. 
The goal was to work on how C# handles data persistence, work with modern libraries, and to build a complete application from scratch.

This is a terminal-based inventory system that manages products through a JSON database. It supports full CRUD operations and can export data to PDF or CSV formats. The UI uses Spectre.Console to provide a clean, interactive command-line experience.

I created this to explore C# fundamentals like classes, static members, file I/O with JSON serialization, and NuGet package integration. It's a practical way to learn the language syntax and project structure in a real-world scenario.

[Software Demo Video](https://www.youtube.com/watch?v=KEVd6ge6g-U)

# Development Environment

Built with .NET 10.0 SDK and Visual Studio. Used the .NET CLI for building and running the application.

Written in C# with three main dependencies: Spectre.Console for the terminal UI, QuestPDF for PDF export, and System.Text.Json for JSON serialization.

# Useful Websites

- [Spectre.Console Documentation](https://spectreconsole.net/)
- [QuestPDF Documentation](https://www.questpdf.com/)
- [Microsoft C# Documentation](https://learn.microsoft.com/en-us/dotnet/csharp/)

# Future Work

- Add search and filter capabilities
- Implement input validation and error handling
- Support for categories and tags
- Add backup/restore functionality