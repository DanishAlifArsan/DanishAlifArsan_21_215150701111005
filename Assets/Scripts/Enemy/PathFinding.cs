using System.Collections.Generic;

public static class PathFinding
{
    private static Dictionary<Point, Node> node;

    private static void Create() {
        node = new Dictionary<Point, Node>;

        foreach (TileScript tileSc in LevelManager.FindObjectOfType<LevelManager>.TileDictionary.Values)
        {
            node.Add(tileSc.GridPosition, new Node(tileSc));
        }
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
