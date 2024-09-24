# Path-Finding Grid Movement in Unity

This Unity project demonstrates a basic grid-based movement system and A* methdod using a pathfinding algorithm. The player navigates from a starting point to an end point through a grid by following a calculated path.

## Features

- **Grid System**: Dynamically generates a grid of customizable size.
- **Pathfinding**: Implements a pathfinding algorithm to find the shortest path from start to end.
- **Player Movement**: The player moves along the calculated path at a defined speed.
- **Dynamic Grid Generation**: Allows for generating different grids by adjusting the number of rows, columns, and scale.

## How It Works

1. **Grid Generation**: A grid is generated using a prefab (`GridPrefab`) at a defined scale, rows, and columns.
2. **Pathfinding**: The player starts at `StartPos` and finds the path to `EndPos` using a pathfinding algorithm.
3. **Player Movement**: Once the path is found, the player object moves smoothly along the path until it reaches the destination.
 

https://github.com/user-attachments/assets/c4baa310-032e-45fa-be34-ac76001851c4

