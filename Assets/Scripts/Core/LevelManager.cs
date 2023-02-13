using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    [SerializeField] private int x;
    [SerializeField] private int y;

    public float TileSize {
        get { return tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    public Dictionary<Grid,TileScript> TileDictionary { get; set; }

    // Start is called before the first frame update
    private void Start()
    {
        CreateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateLevel() {
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
    }
}
