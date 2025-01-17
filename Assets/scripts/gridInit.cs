using System.Collections.Generic;
using UnityEngine;

public class GridInit : MonoBehaviour{
    
    Vector2Int startPosition;

    public int lenght;
    public int height;

    public float cellRadius;
    float cellDiameter => cellRadius * 2;

    public LayerMask obstacleLayer;
    public float cellCheckRadius;
    Grid grid;

    AStarAlg aStarAlg;

    public Vector2 startPosForAStar;
    public Vector2 targetPosForAStar;
    void Start(){
       startPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
       InstantiateGrid();

       
    }

    void InstantiateGrid(){
        grid = new(startPosition, lenght, height);

        grid.PopulateGrid(cellRadius, startPosition);
        for(int x = 0; x < lenght; x++){
            for(int y = 0; y < height; y++){
                grid.AddNeighbors(grid.cells[x, y], 1.5f, startPosition);
                CheckCellStatus(grid.cells[x, y]);
            }
        }

         
        /*for (int x = 0; x < lenght; x++) {
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
        }*/

        aStarAlg = new();

        aStarAlg.Astar(grid, startPosForAStar, targetPosForAStar);
    }

    void CheckCellStatus(Cell cell){
        if(Physics2D.OverlapCircle(cell.position, cellCheckRadius, obstacleLayer)){
            cell.isWalkable = false;
        }else{
            cell.isWalkable = true;
        }
    }

    void OnDrawGizmos(){
        if(Application.isPlaying){
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(new Vector3(startPosition.x, startPosition.y), new Vector2(lenght, height));

            foreach(Cell cell in grid.neighbors.Keys){
                Gizmos.color = cell.isWalkable ? Color.green : Color.red;
                
                Gizmos.DrawWireCube(cell.position, new Vector2(cellDiameter, cellDiameter));
            }
        }
    }
}
