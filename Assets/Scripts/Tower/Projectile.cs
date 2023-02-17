using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;
    // private float direction;
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
            Debug.Log("hit enemy");
            collision.GetComponent<Enemy>().TakeDamage(1);
        }
    }

    public void MoveToTarget(Enemy targettedEnemy) {
        lifetime = 0;
        // direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        //set direction
        float movementSpeed = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targettedEnemy.transform.position, movementSpeed);

        Vector2 direction = targettedEnemy.transform.position - transform.position;

        float angle = Mathf.Atan2(direction.x,direction.y) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

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
