using UnityEngine;

#pragma warning disable IDE1006 // Naming Styles
public class Cell
{
   public LayerMask objectLayer;
   public Vector2 position;
   public bool isWalkable;

   public int gCost; // distance to target cell
   public int hCost; // distance from start cell


    public int fCost => gCost + hCost;

    public float cellRadius; //Even though each cell is a square. Radius is just half the length of the cell

    public Cell(){
        
    }
}
#pragma warning restore IDE1006 // Naming Styles