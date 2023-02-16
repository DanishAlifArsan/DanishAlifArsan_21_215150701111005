using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Grid GridPosition {get; private set;}

    public TileScript TilesReference { get; private set; }

    public Vector2 WorldPosition { get; set; }

    public Node(TileScript tilesReference) {
        this. TilesReference = tilesReference;
        this.GridPosition = tilesReference.GridPosition;
        this.WorldPosition = tilesReference.WorldPosition;
    }

    public Node Parent { get; private set; }
    public int GCost { get; private set; }
    public int HCost { get; set; }
    public int FCost { get; set; }

    public void CalculateValue(Node parent, Node goal, int gCost) {
        this.Parent = parent;
        this.GCost = parent.GCost + gCost;
        this.HCost = (Mathf.Abs(GridPosition.x - goal.GridPosition.x) + Mathf.Abs(GridPosition.y - goal.GridPosition.y)) * 10 ;
        this.FCost = GCost + HCost;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
