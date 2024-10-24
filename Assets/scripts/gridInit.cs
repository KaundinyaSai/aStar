using System.Collections.Generic;
using UnityEngine;

public class GridInit : MonoBehaviour{
    
    Vector2 startPosition;

    public int lenght;
    public int height;

    public float cellRadius;
    float cellDiameter => cellRadius * 2;
    Grid grid;
    void Start(){
       startPosition = transform.position;
       InstantiateGrid();
    }

    void InstantiateGrid(){
        grid = new(startPosition, lenght, height);

        grid.PopulateGrid(cellRadius, startPosition);
        for(int x = 0; x < lenght; x++){
            for(int y = 0; y < height; y++){
                grid.AddNeighbors(grid.cells[x, y], 1.5f);
            }
        }

         
        for (int x = 0; x < lenght; x++) {
            for (int y = 0; y < height; y++) {
                Cell cell = grid.cells[x, y];
                string logMessage = $"Neighbors of cell ({x}, {y}): ";

                // Build the neighbor list string
                List<string> neighborCoords = new List<string>();
                foreach (var neighbor in grid.neighbors[cell]) {
                    neighborCoords.Add($"({neighbor.position.x}, {neighbor.position.y})");
                }

                // Join the coordinates and add to the log
                logMessage += string.Join(", ", neighborCoords);
                Debug.Log(logMessage);
            }
        }
    }

    void CheckCellStatus(Cell cell){
        
    }

    void OnDrawGizmos(){
        if(Application.isPlaying){
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(startPosition, new Vector2(lenght, height));

            foreach(Cell cell in grid.neighbors.Keys){
                Gizmos.DrawWireCube(cell.position, new Vector2(cellDiameter, cellDiameter));
            }
        }
    }
}
