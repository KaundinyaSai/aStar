using System;
using System.Collections.Generic;
using UnityEngine;

public class AStarAlg : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 targetPos;

    GridInit grid;

    void Start(){
        startPos = WorldToCellPos(startPos);
        targetPos = WorldToCellPos(targetPos);
    }
    
    Vector2 WorldToCellPos(Vector2 worldPos){
        return new Vector2(Mathf.FloorToInt(worldPos.x) - 0.5f, Mathf.FloorToInt(worldPos.y) - 0.5f); // -0.5f because pos of each cell ends with .5 not with .0
    }

    void Astar(Vector2 startPos, Vector2 targetPos){
        List<Cell> visitedList = new();
        List<Cell> openList = new();

        Cell startCell = grid.cells[Mathf.FloorToInt(startPos.x), Mathf.FloorToInt(startPos.y)];

        openList.Add(startCell);

        while(openList.Count > 0){
            for(int i = 0; i < openList.Count; i++){
                for(int j = 0; j < i; j++){
                    if(grid.cells[i,j].isWalkable && !openList.Contains(grid.cells[i,j]) && !visitedList.Contains(grid.cells[i,j])){
                        openList.Add(grid.cells[i,j]);
                    }
                }
            }
        }
    }


}
