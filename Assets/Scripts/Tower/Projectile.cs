using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;
    private float direction;
    private float lifetime;

    private BoxCollider2D boxCollider;
    // private Animator anim;

    // Start is called before the first frame update
    private void Awake()
    {   
        boxCollider = GetComponent<BoxCollider2D>();
        // anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (hit) {
            return;
        }

        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 5) {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        hit = true;
        boxCollider.enabled = false;
        // anim.SetTrigger("explosion");
        Deactivate();

        if(collision.tag == "Enemy") {
            // collision.GetComponent<Health>().TakeDamage(1);
        }
    }

    public void SetDirection(float _direction) {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        // float localScaleX = targettedEnemy.transform.localScale.x;
        // if (Mathf.Sign(localScaleX) != _direction) {
        //     localScaleX = -localScaleX;
        //     speed = -speed;
        // }

        // targettedEnemy.transform.localScale = new Vector3(localScaleX, transform.localScale.y,targettedEnemy.transform.localScale.z);
    }

    private void Deactivate() {
        gameObject.SetActive(false);   
    }
}
