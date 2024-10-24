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
        
    }


}
