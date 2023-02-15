using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Point GridPosition {get; private set;}

    public TileScript TilesReference { get; private set; }

    public Node(TileScript tilesReference) {
        this. TilesReference = tilesReference;
        this.GridPosition = tilesReference.GridPosition;
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
