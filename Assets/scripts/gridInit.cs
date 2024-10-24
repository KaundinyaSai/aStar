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

                AddNeighbors(cells[i, j]);
                /*string message = $"Cell at {cells[i, j].position} has the following neighbors: ";
                foreach (var neighbor in cells[i, j].neighbors)
                {
                    if (neighbor != null)
                        message += $"{neighbor.position}, ";  
                   
                }

                // Log the full message once
                Debug.Log(message);*/
            }
        }
        
    }

    void AddNeighbors(Cell cell){
        float cellDiameter = cell.cellRadius * 2;
        // List of offsets to find neighbors based on world positions
        Vector2[] neighborOffsets = new Vector2[] {
            new Vector2(-cellDiameter, 0),  new Vector2(cellDiameter, 0),   // Left, Right
            new Vector2(0, -cellDiameter),  new Vector2(0, cellDiameter),   // Down, Up
            new Vector2(-cellDiameter, -cellDiameter), new Vector2(-cellDiameter, cellDiameter),  // Bottom-left, Top-left
            new Vector2(cellDiameter, -cellDiameter),  new Vector2(cellDiameter, cellDiameter)    // Bottom-right, Top-right
        };

        foreach (Vector2 offset in neighborOffsets)
        {
            // Calculate the neighbor's world position by applying the offset to the current cell's position
            Vector2 neighborPos = cell.position + offset;

            Debug.Log($"Current cell: {cell.position}, Neighbor Position: {neighborPos}");

            // Now find the corresponding grid indices for the neighbor position
            Vector2Int neighborIndex = new Vector2Int(
                Mathf.FloorToInt(neighborPos.x - startPosition.x),
                Mathf.FloorToInt(neighborPos.y - startPosition.y)
            );

            // Check if the neighbor is within the grid bounds
            if (neighborIndex.x >= 0 && neighborIndex.x < cells.GetLength(0) && neighborIndex.y >= 0 && neighborIndex.y < cells.GetLength(1))
            {
                Cell neighbor = cells[neighborIndex.x, neighborIndex.y];

                // Only add walkable neighbors
                if (neighbor != null && neighbor.isWalkable)
                    cell.neighbors.Add(neighbor);
                else{
                    cell.neighbors.Add(null);
                }
            }
            else
            {
                // Add null if the neighbor is out of bounds
                cell.neighbors.Add(null);
            }
        }
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

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            foreach(Cell neighbor in cells[5,5].neighbors){
                Debug.Log($"Neigbor at: {neighbor.position}");
            }
        }
    }

    
}
