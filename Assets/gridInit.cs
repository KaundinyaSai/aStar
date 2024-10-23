using System;
using Unity.VisualScripting;
using UnityEngine;

public class GridInit : MonoBehaviour
{
    Cell[,] cells; //2d cells array
    public int length;
    public int height;
    public LayerMask obstacleLayer; 
    public float cellRadius;
    public Vector2Int startPosition;
    public float checkOffset;
    void Start(){
        if(cells.Length > 0){
            Array.Clear(cells, 0, cells.Length); // Clear the array bcause we populated it in the OnDrawGizmosSelected
        }else{
            PopulateGrid();
        }
        
    }

    void PopulateGrid() {
        cells = new Cell[length, height];
        for (int i = 0; i < length; i++) {
            for (int j = 0; j < height; j++) {
                cells[i, j] = new Cell();

                // Set the position of the cell
                Vector2 cellPosition = new Vector2(i, j) + startPosition; 
                cellPosition += new Vector2(cellRadius, cellRadius); // Offset by the cell radius to make it start at bottom right

                cells[i, j].position = cellPosition;
                cells[i, j].cellRadius = cellRadius;

                // Check for obstacles within the cell radius
                if (Physics2D.OverlapCircle(cellPosition, cells[i, j].cellRadius - checkOffset, obstacleLayer)) {
                    cells[i, j].isWalkable = false;
                } else {
                    cells[i, j].isWalkable = true;
                }
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

}
