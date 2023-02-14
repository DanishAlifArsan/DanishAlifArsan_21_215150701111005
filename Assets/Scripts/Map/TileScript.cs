using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public Grid GridPosition { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(Grid GridPosition) {
        this.GridPosition = GridPosition;
    }

   public Vector2 WorldPosition { 
        get {
            return new Vector2(transform.position.x + (GetComponent<SpriteRenderer>().bounds.size.x / 2), 
            transform.position.y - (GetComponent<SpriteRenderer>().bounds.size.y / 2));
        }
    }

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) 
        {
            Instantiate(GameManager.FindObjectOfType<GameManager>().Tower,transform.position,Quaternion.identity);
        }
    }
}
