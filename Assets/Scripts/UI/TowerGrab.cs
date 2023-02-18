using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGrab : MonoBehaviour
{
    private SpriteRenderer spriteRend;

    public bool IsVisible { get; set; }

    // Start is called before the first frame update
    private void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        MouseFollow();
    }

    private void MouseFollow() {
        if (spriteRend.enabled)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        
    }

    public void Activate(Sprite sprite) {
        this.spriteRend.sprite = sprite;
        spriteRend.enabled = true;

        IsVisible = true;
    }

    public void Deactivate() {
        spriteRend.enabled = false;
        GameManager.FindObjectOfType<GameManager>().TowerBtn = null;

        IsVisible = false;
    }
}
