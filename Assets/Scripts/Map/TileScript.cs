using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
            if (!EventSystem.current.IsPointerOverGameObject() && GameManager.FindObjectOfType<GameManager>().TowerBtn != null) {
                GameObject tower = Instantiate(GameManager.FindObjectOfType<GameManager>().TowerBtn.Tower,transform.position,Quaternion.identity);
                tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.y;

                tower.transform.SetParent(transform);

                GameManager.FindObjectOfType<GameManager>().BuyTower();
            }

            
        }
    }
}
