using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class PathFinding
{
    private static Dictionary<Grid, Node> node;

    private static void Create() {
        node = new Dictionary<Grid, Node>();

        foreach (TileScript tileSc in LevelManager.FindObjectOfType<LevelManager>().TileDictionary.Values)
        {
            node.Add(tileSc.GridPosition, new Node(tileSc));
        }
    }

    public static Stack<Node> GetPath(Grid start, Grid goal) {
        if (node == null)
        {
            Create();
        }
        HashSet<Node> openList = new HashSet<Node>();
        HashSet<Node> closedList = new HashSet<Node>();

        Stack<Node> finalPath = new Stack<Node>();

        Node currentNode = node[start];

        openList.Add(currentNode);

        while (openList.Count > 0)
        {
            for (int i = -1; i <= 1; i++)        {
                for (int j = -1; j <= 1; j++)
                {
                    Grid neighborPosition = new Grid(currentNode.GridPosition.x - i, currentNode.GridPosition.y - j);
              
                    if (LevelManager.FindObjectOfType<LevelManager>().InBounds(neighborPosition) && LevelManager.FindObjectOfType<LevelManager>().TileDictionary[neighborPosition].Walkable && neighborPosition != currentNode.GridPosition)
                    {
                        int gCost = 0;

                        if (Mathf.Abs(i-j) == 1)
                        {
                            gCost = 10;
                        } else {
                            gCost = 14;
                        }
                    
                        Node neighbor = node[neighborPosition];

                        if(openList.Contains(neighbor)) {
                            if (currentNode.GCost + gCost < neighbor.GCost)
                            {
                                neighbor.CalculateValue(currentNode, node[goal], gCost);
                            }
                        } else if (!closedList.Contains(neighbor))
                        {
                            openList.Add(neighbor);
                            neighbor.CalculateValue(currentNode, node[goal], gCost);
                        }

                    // for debug only
                    // neighbor.TilesReference.SpriteRend.color = Color.black;

                    }
                    //for debug only
                    // Debug.Log(neighborPosition.x + "," + neighborPosition.y);

                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (openList.Count > 0)
            {
                currentNode = openList.OrderBy(n => n.FCost).First();
            }

            if (currentNode == node[goal])
            {   
                while (currentNode.GridPosition != start)
                {
                    finalPath.Push(currentNode);
                    currentNode = currentNode.Parent;
                }
                break;
            }
        }

        return finalPath;

        // //for debug only
        // GameObject.Find("AStarDebugger").GetComponent<AStarDebugger>().DebugPath(openList,closedList);

      

    }
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
