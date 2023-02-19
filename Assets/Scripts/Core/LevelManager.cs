using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header ("Tiles")]
    [SerializeField] private GameObject tile;
    [SerializeField] private int x;
    [SerializeField] private int y;

    [Header ("Flag location")]
    [SerializeField] private GameObject enterPoint;
    [SerializeField] private GameObject exitPoint;

    private Grid enterLocation;
    private Grid exitLocation;

    public EnemySpawn SpawnPoint { get; set; }

    private Stack<Node> path;

    public Stack<Node> Path { 
        get{
            if (path == null) {
                GeneratePath();
            }

            return new Stack<Node>(new Stack<Node>(path));
        }
    }

    public float TileSize {
        get { return tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    public Dictionary<Grid,TileScript> TileDictionary { get; set; }

    // Start is called before the first frame update
    private void Start()
    {
        TileDictionary = new Dictionary<Grid,TileScript>();

        Vector3 topLeft = Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height));

        Vector3 maxTiles = Vector3.zero;

        for (int i = 0; i <= x; i++)
        {
            for (int j = 0; j <= y; j++)
            {
                TileScript newTile =  Instantiate(tile).GetComponent<TileScript>();
                newTile.transform.position = new Vector3(topLeft.x + (TileSize * i), topLeft.y - (TileSize * j), 0);

                newTile.Setup(new Grid(i,j));

                TileDictionary.Add(new Grid(i,j),newTile);
            }
        }

        maxTiles = TileDictionary[new Grid(x-1,y-1)].transform.position;

        FlagLocation();
    }

    private void FlagLocation() {
        enterLocation = new Grid(0,0);
        GameObject temp = (GameObject)Instantiate(enterPoint, TileDictionary[enterLocation].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        SpawnPoint = temp.GetComponent<EnemySpawn>();
        SpawnPoint.name = "Enter Point";

        exitLocation = new Grid(19, 5);
        Instantiate(exitPoint, TileDictionary[exitLocation].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
    }

    public bool InBounds(Grid position) {
        return position.x >= 0 && position.y >= 0;
    }

    public void GeneratePath() {
        path = PathFinding.GetPath(enterLocation, exitLocation);
    }
}
