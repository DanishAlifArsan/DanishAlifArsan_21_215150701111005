using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour
{
    public Grid GridPosition { get; private set; }

    private Color32 invalidColor = new Color32(255,118,118,255);

    private Color32 validColor = new Color32(96,255,90,255);

    private SpriteRenderer spriteRend;

    // Start is called before the first frame update
    private void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
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
        spriteRend.color = invalidColor;
        
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

    private void OnMouseExit() {
        spriteRend.color = Color.white;
    }
}
