# Path-Finding Grid Movement in Unity

This Unity project demonstrates a basic grid-based movement system, featuring both **A* algorithm** and **Breadth-First Search (BFS)** methods for pathfinding. The player navigates from a starting point to an end point through a dynamically generated grid by following a calculated path.

## Features

- **Grid System**: Dynamically generates a grid of customizable size.
- **Pathfinding Algorithms**: Implements both A* and BFS algorithms to find the shortest path from the start point to the end point.
  - A* Algorithm (PathFinderAStar.cs)
  - Breadth-First Search (PathFinder.cs)

https://github.com/user-attachments/assets/63e82436-e08f-4b86-b576-f7dea3c05f97
 
- **Player Movement**: The player moves along the calculated path at a defined speed.
- **Dynamic Grid Generation**: Easily generate different grids by adjusting the number of rows, columns, and scale in the Unity Inspector.
- **Inspector Controls**: Toggle between A* and BFS algorithms in the Unity Inspector to compare their behavior.

## How It Works

### Grid Generation
A grid is generated using a prefab (`GridPrefab`) with customizable parameters:
- **Rows**: Number of rows in the grid.
- **Columns**: Number of columns in the grid.
- **Scale**: Determines the size of each grid cell.

### Pathfinding
The player starts at a defined `StartPos` and finds the path to the `EndPos` using the selected pathfinding algorithm (A* or BFS). The pathfinding algorithms calculate the optimal path across the grid and return a list of grid cells that the player should traverse.

### Player Movement
Once the path is calculated, the player object moves smoothly from the start position to the end position along the calculated path. The speed of movement is customizable in the Unity Inspector.

## Inspector Controls

The Unity Inspector allows for easy control over grid generation and pathfinding execution:
- **Grid Parameters**: Adjust the number of rows, columns, and scale for grid generation.
- **Pathfinding Algorithms**: Choose between A* and BFS by clicking corresponding buttons in the Inspector.
- **Buttons**:
  - `Initialize`: Generates the grid and places the player at the start position.
  - `ExecutePathfinding`: Finds the path and moves the player along it using the selected algorithm.

## Getting Started

### Prerequisites
- Unity 2020.3 or higher

https://github.com/user-attachments/assets/21d89987-c64f-440f-819d-346f347e36ce


