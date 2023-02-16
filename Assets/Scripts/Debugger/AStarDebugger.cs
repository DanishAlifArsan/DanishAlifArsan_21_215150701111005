using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarDebugger : MonoBehaviour
{
    private TileScript start, goal;
    [SerializeField] private Sprite blank;
    [SerializeField] private GameObject Arrow;
    [SerializeField] private GameObject debugTile;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckTile();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PathFinding.GetPath(start.GridPosition, goal.GridPosition);
        }
    }

    private void CheckTile(){
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null) {
                TileScript temp = hit.collider.GetComponent<TileScript>();

                if (temp != null)
                {
                     if (start == null)
                     {
                        start = temp;
                        createDebugTile(start.WorldPosition,new Color32(255,132,0,255));
                     } else if (goal == null) {
                        goal = temp;
                        createDebugTile(goal.WorldPosition, new Color32(255,132,0,255));
                     }
                }
            }
        }
    }

    public void DebugPath(HashSet<Node> openList, HashSet<Node> closedList) {
        foreach (Node node in openList)
        {
            if (node.TilesReference != start)
            {   
                // node.TilesReference.SpriteRend.color = Color.cyan;
                // node.TilesReference.SpriteRend.sprite = blank;
            }    

            // PointToParent(node, TilesReference.WorldPosition);
        }

        foreach (Node node in closedList)
        {
            if (node.TilesReference != start && node.TilesReference != goal) 
            {   
                createDebugTile(node.TilesReference.WorldPosition, Color.blue);
                // node.TilesReference.SpriteRend.color = Color.cyan;
                // node.TilesReference.SpriteRend.sprite = blank;
            }    

            // PointToParent(node, TilesReference.WorldPosition);
        }
    }
    

    private void PointToParent(Node node, Vector2 position) {
        if (node.Parent != null)
        {
           GameObject arrow = (GameObject)Instantiate(Arrow, position, Quaternion.identity);

            if (node.GridPosition.x < node.Parent.GridPosition.x && node.GridPosition.y == node.Parent.GridPosition.y)
            {
                // arrow.transform.eulerAngels = new Vector3(0,0,0);
            } 
        }

        
    }

    private void createDebugTile(Vector3 WorldPosition, Color32 color) {
        GameObject dbgTile = (GameObject)Instantiate(debugTile, WorldPosition, Quaternion.identity);

        dbgTile.GetComponent<SpriteRenderer>().color = color;
    }
}
