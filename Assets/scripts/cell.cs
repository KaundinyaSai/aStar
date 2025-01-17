using UnityEngine;
using System.Collections.Generic;

#pragma warning disable IDE1006 // Naming Styles
public class Cell
    {
    public LayerMask objectLayer;
    public Vector2 position;
    public bool isWalkable;
    public float gCost; // distance from start Cell
    public float hCost; // distance to target Cell

    public Cell parent; // For path reconstruction

    public float fCost => gCost + hCost;

    public float cellRadius; //Even though each cell is a square. Radius is just half the length of the cell

    public Cell(Vector2 position){
        this.position = position;
    }
}
#pragma warning restore IDE1006 // Naming Styles