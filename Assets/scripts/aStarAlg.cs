using System;
using System.Collections.Generic;
using UnityEngine;

public class AStarAlg
{
    Vector2 WorldToCellPos(Vector2 worldPos){
        return new Vector2(Mathf.FloorToInt(worldPos.x), Mathf.FloorToInt(worldPos.y)); 
    }

    public void Astar(Grid grid,Vector2 startPos, Vector2 targetPos) {
        List<Cell> openList = new List<Cell>();
        HashSet<Cell> closedList = new HashSet<Cell>(); // Use HashSet for better performance

        Vector2 startCellPos = WorldToCellPos(startPos);
        Vector2 targetCellPos = WorldToCellPos(targetPos);

        Cell startCell = grid.cells[(int)startCellPos.x, (int)startCellPos.y];
        Cell targetCell = grid.cells[(int)targetCellPos.x, (int)targetCellPos.y];

        openList.Add(startCell);

        while (openList.Count > 0) {
            // Find the cell with the lowest fCost
            Cell currentCell = openList[0];
            for (int i = 1; i < openList.Count; i++) {
                if (openList[i].fCost < currentCell.fCost || 
                    (openList[i].fCost == currentCell.fCost && openList[i].hCost < currentCell.hCost)) {
                    currentCell = openList[i];
                }
            }

            // Remove currentCell from openList and add to closedList
            openList.Remove(currentCell);
            closedList.Add(currentCell);

            // Check if we've reached the target
            if (currentCell == targetCell) {
                List<Cell> path = ReconstructPath(targetCell);

                foreach(Cell cell in path){
                    Debug.Log($"({cell.position.x}, {cell.position.y})");
                }
                return;
            }

            // Process neighbors
            foreach (Cell neighbor in grid.neighbors[currentCell]) {
                if (closedList.Contains(neighbor) || !neighbor.isWalkable) {
                    continue;
                }

                // Calculate potential gCost for the neighbor
                float tentativeGCost = currentCell.gCost + Vector2.Distance(currentCell.position, neighbor.position);

                // If this path to neighbor is better or neighbor is not in openList
                if (tentativeGCost < neighbor.gCost || !openList.Contains(neighbor)) {
                    SetCosts(neighbor, currentCell, targetCell);
                    neighbor.parent = currentCell;
                    
                    if (!openList.Contains(neighbor)) {
                        openList.Add(neighbor);
                    }
                }
            }
        }
    }


    void SetCosts(Cell cell, Cell startCell, Cell targetCell){
        cell.gCost = Vector2.Distance(cell.position, startCell.position);
        cell.hCost = Vector2.Distance(cell.position, targetCell.position);
    }

    List<Cell> ReconstructPath(Cell targetCell) {
        List<Cell> path = new List<Cell>(); // List to store the final path
        Cell current = targetCell; // Start from the target cell

        while (current != null) {
            path.Add(current); // Add the current cell to the path
            current = current.parent; // Move to the parent cell
        }

        path.Reverse(); // Reverse the list to get the path from start to target
        return path; // Return the reconstructed path
    }
}