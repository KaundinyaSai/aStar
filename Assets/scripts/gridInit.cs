using System;
using System.Linq; // For LINQ methods
using UnityEngine;

public class GridInit : MonoBehaviour
{
    [HideInInspector] public Cell[,] cells; //2d cells array
    public int length;
    public int height;
    public LayerMask obstacleLayer; 
    public float cellRadius;
    public Vector2Int startPosition;
    public float checkOffset;
    void Start(){
        
        PopulateGrid();
    }

    void PopulateGrid() {
        cells = new Cell[length, height];
        for (int i = 0; i < length; i++) {
            for (int j = 0; j < height; j++) {
                Vector2 cellPosition = new Vector2(i, j) + startPosition; // Set the cell position
                cellPosition += new Vector2(cellRadius, cellRadius); // Offset by the cell radius to make it start at bottom right

                cells[i, j] = new Cell(cellPosition)
                {
                    position = cellPosition,
                    cellRadius = cellRadius
                };

                // Check for obstacles within the cell radius  
                if (Physics2D.OverlapCircle(cellPosition, cells[i, j].cellRadius - checkOffset, obstacleLayer)) {
                    cells[i, j].isWalkable = false;
                } else {
                    cells[i, j].isWalkable = true; 
                }

                AddNeighbors(cells[i ,j], 1.5f, length, height);

                LogNeighbors(cells[i ,j]);
            }
        }
        
    }

    void AddNeighbors(Cell cell, float threshold, float gridWidth, float gridHeight){
        for(int x = 0; x < gridWidth; x++){
            for(int y = 0; y < gridHeight; y++){
                float distance = Vector2.Distance(cell.position, cells[x, y].position);

                if(distance <= threshold){
                    cell.neighbors.Add(cells[x, y]);
                }
            }
        }
    }

    void LogNeighbors(Cell cell) {
        // Start with the header for the log 
        string neighborLog = $"Neighbors of cell at ({cell.position.x}, {cell.position.y}): "; 

        // Check if there are any neighbors
        if (cell.neighbors.Count > 0) {
            foreach (var neighbor in cell.neighbors) {
                // Append each neighbor's position to the log
                neighborLog += $"({neighbor.position.x}, {neighbor.position.y}) ";
            }
        } else {
            neighborLog += "None"; // Handle the case where there are no neighbors
        }

        // Log the final string
        Debug.Log(neighborLog);
    }

    
    void OnDrawGizmosSelected(){
        if (cells == null || cells.Length == 0) {
            PopulateGrid(); // Populate the grid for checking
        }
        
        Gizmos.DrawWireCube((Vector2)transform.position - startPosition, new Vector3(length, height));

        foreach(Cell cell in cells){
            Gizmos.color = cell.isWalkable ? Color.green : Color.red;

            Gizmos.DrawWireCube(cell.position, new Vector3(cell.cellRadius * 2, cell.cellRadius * 2));
        }
    }
}
