# OOP-ShapeDrawer

A Windows Forms application for creating, manipulating, and managing geometric shapes. Built using C# and object-oriented programming principles, this interactive drawing tool demonstrates inheritance, polymorphism, and event-driven programming.

## Features

- **Multiple Shape Types**: Draw ellipses, rectangles, and squares with ease
- **Color Controls**: Select colors and fill shapes with your preferred palette
- **Edit Capabilities**: Undo/Redo functionality for all operations
- **Persistence**: Save and load your work using JSON serialization
- **Shape Management**: Erase unwanted shapes with a dedicated eraser tool
- **Analytics**: Get information about drawn shapes (area, count, etc.)

## Implementation Details

This project showcases several important OOP concepts:

- **Inheritance Hierarchy**: Abstract base `Shape` class with derived implementations for different geometric shapes
- **Polymorphism**: Each shape implements its own drawing, area calculation, and point containment logic
- **Event-Driven Programming**: Custom events for tracking shape changes
- **Command Pattern**: Implementation of undo/redo stack for operation history
- **Serialization**: JSON-based state persistence using Newtonsoft.Json
- **Clean Architecture**: Separation of UI and business logic

## Class Structure

- `Shape` - Abstract base class defining common shape operations
- `Ellipse`, `Rectangle`, `Square` - Concrete shape implementations
- `ShapeManager` - Manages collection of shapes
- `ShapeChangedEventArgs` - Custom event arguments for shape operations

## Getting Started

### Prerequisites

- Visual Studio 2019 or later
- .NET Framework 4.7.2 or later

### Installation

1. Clone the repository
   ```
   git clone https://github.com/yourusername/OOP-ShapeDrawer.git
   ```

2. Open the solution in Visual Studio
   ```
   cd ShapeStudio
   start ShapeStudio.sln
   ```

3. Build and run the application
   ```
   Press F5 or click the Run button in Visual Studio
   ```

## Usage Guide

1. Select a shape type (ellipse, rectangle, or square) from the toolbar
2. Click and drag on the canvas to create the shape
3. Use the color picker to change the shape color
4. Click "Fill" to fill the selected shape with the current color
5. Use "Eraser" to remove unwanted shapes
6. Save your work with the "Save" button and load it later with "Load"
7. Use "Undo" and "Redo" to navigate through your editing history
8. Click "Info" to view statistics about your shapes


## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

