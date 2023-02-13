using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    [SerializeField] private float x;
    [SerializeField] private float y;

    public float TileSize {
        get { return tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

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

        Vector3 topLeft = Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height));

        for (int i = 0; i <= x; i++)
        {
            for (int j = 0; j <= y; j++)
            {
                GameObject newTile =  Instantiate(tile);
                newTile.transform.position = new Vector3(topLeft.x + (TileSize * i), topLeft.y - (TileSize * j), 0);
            }
        }
    }
}
