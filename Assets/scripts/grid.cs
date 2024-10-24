using System;
using System.Collections.Generic;
using UnityEngine;

public class Grid {

    public Cell[,] cells; // 2D array of cells to store the cells
    public Dictionary<Cell, List<Cell>> neighbors = new(); // For storing the neigbors of each cell. The cell is the key and the list of neigbors is the value.

    public Vector2 startPosition;
    public int width;
    public int height;

   public Grid(Vector2 startPosition, int width, int height) {
        this.startPosition = startPosition;
        this.width = width;
        this.height = height;

        cells = new Cell[width, height];
    }

    public void PopulateGrid(float cellRadius, Vector2 gridStartingPoint){
        for(int x = 0; x < width; x++){
            for(int y = 0; y < height; y++){
                cells[x, y] = new Cell(new Vector2(x, y));
                neighbors.Add(cells[x, y], new List<Cell>());
                cells[x, y].position = gridStartingPoint + new Vector2(x, y);
                cells[x, y].cellRadius = cellRadius;
            }
        }
    }

    public void AddNeighbors(Cell cell, float threshold) {
        int x = (int)cell.position.x; // Get the integer x position
        int y = (int)cell.position.y; // Get the integer y position

        // Possible neighboring positions
        Vector2[] neighborPositions = {
            new Vector2(x + 1, y),   // Right
            new Vector2(x - 1, y),   // Left
            new Vector2(x, y + 1),   // Up
            new Vector2(x, y - 1),   // Down
            new Vector2(x + 1, y + 1), // Up-right diagonal
            new Vector2(x - 1, y + 1), // Up-left diagonal
            new Vector2(x + 1, y - 1), // Down-right diagonal
            new Vector2(x - 1, y - 1)  // Down-left diagonal
        };

        foreach (var neighborPos in neighborPositions) {
            // Check if the neighbor is within grid bounds
            if (neighborPos.x >= 0 && neighborPos.x < width && 
                neighborPos.y >= 0 && neighborPos.y < height) {

                Cell neighbor = cells[(int)neighborPos.x, (int)neighborPos.y];
                
                // Add to the neighbor list if within the threshold distance
                float distance = Vector2.Distance(cell.position, neighbor.position); 
                if (distance < threshold) {
                    neighbors[cell].Add(neighbor);
                }
            }
        }
    }
}
