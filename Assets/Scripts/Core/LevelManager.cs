using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    [SerializeField] private float x;
    [SerializeField] private float y;

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
        
        float tileSize = tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x;

        for (int i = 0; i <= x; i++)
        {
            for (int j = 0; j <= y; j++)
            {
                GameObject newTile =  Instantiate(tile);
                newTile.transform.position = new Vector3(tileSize * i, tileSize * j, 0);
            }
        }
    }
}
