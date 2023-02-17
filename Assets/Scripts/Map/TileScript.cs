using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour
{
    public Grid GridPosition { get; private set; }
    
    public bool IsEmpty { get; set; }

    [SerializeField] private Color32 invalidColor;

    [SerializeField] private Color32 validColor;

    private SpriteRenderer spriteRend;

    private Tower tower;

    // public SpriteRenderer SpriteRend { get; set; }

    public bool Walkable { get; set; }

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
        Walkable = true;
        IsEmpty = true;
        this.GridPosition = GridPosition;
    }

   public Vector2 WorldPosition { 
        get {
            return new Vector2(transform.position.x + (GetComponent<SpriteRenderer>().bounds.size.x / 2), 
            transform.position.y - (GetComponent<SpriteRenderer>().bounds.size.y / 2));
        }
    }

    private void OnMouseOver() {
        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.FindObjectOfType<GameManager>().TowerBtn != null) {
            if (IsEmpty)
            {
                spriteRend.color = validColor;
            } 
            
            if (!IsEmpty)
            {
                spriteRend.color = invalidColor;
            }
            else if (Input.GetMouseButtonDown(0)) 
            {
                GameObject tower = Instantiate(GameManager.FindObjectOfType<GameManager>().TowerBtn.Tower,transform.position,Quaternion.identity);
                tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.y;

                tower.transform.SetParent(transform);

                this.tower = tower.transform.GetChild(0).GetComponent<Tower>();

                this.tower.Cost = GameManager.FindObjectOfType<GameManager>().TowerBtn.Cost;

                Walkable = false;
                IsEmpty = false;

                GameManager.FindObjectOfType<GameManager>().BuyTower();
            }
        }
        else if (!EventSystem.current.IsPointerOverGameObject() && GameManager.FindObjectOfType<GameManager>().TowerBtn == null && Input.GetMouseButtonDown(0))
        {
            if (this.tower != null)
            {
                GameManager.FindObjectOfType<GameManager>().SelectTower(this.tower);
            } else {
                GameManager.FindObjectOfType<GameManager>().DeselectTower();
            }
        }    
    }

    private void OnMouseExit() {
        spriteRend.color = Color.white;
    }
}
