using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damage;
    // [SerializeField] private float speed;
    // private bool hit;
    // private float direction;
    // private float lifetime;

    private BoxCollider2D boxCollider;
    // private Animator anim;

    private Enemy target;
    private Tower towerParent;

    public void Initialize(Tower towerParent) {
        this.towerParent = towerParent;
        this.target = towerParent.TargettedEnemy;
    }

    // Start is called before the first frame update
    private void Awake()
    {   
        boxCollider = GetComponent<BoxCollider2D>();
        // anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        // if (hit) {
        //     return;
        // }

        MoveToTarget();

        // targettedEnemy = Tower.FindObjectOfType<Tower>().enemyQueue.Dequeue();

        // if(Tower.FindObjectOfType<Tower>().enemyQueue.Count > 0) {
        //     float movementSpeed = speed * Time.deltaTime;

        //     transform.position = Vector3.MoveTowards(transform.position, targettedEnemy.transform.position, movementSpeed);
        //     Vector2 direction = targettedEnemy.transform.position - transform.position;

        //     float angle = Mathf.Atan2(direction.x,direction.y) * Mathf.Rad2Deg;

        //     transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // }

        // lifetime += Time.deltaTime;
        // if (lifetime > 5) {
        //     gameObject.SetActive(false);
        // }
    }

    private void MoveToTarget(){
        if (target != null && target.gameObject.activeInHierarchy)
        {
            // float movementSpeed = speed * Time.deltaTime;

            Debug.Log("moving");

            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * towerParent.ProjectileSpeed);

            Vector2 direction = target.transform.position - transform.position;

            float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        } else if (target == null) {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // hit = true;
        // boxCollider.enabled = false;
        // anim.SetTrigger("explosion");
        // Deactivate();

        if(collision.tag == "Enemy") {
            Debug.Log("hit enemy");
            
            gameObject.SetActive(false);
            collision.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    // public void SetVisibility() {
    //     // lifetime = 0;
    //     // direction = _direction;
    //     gameObject.SetActive(true);
    //     hit = false;
    //     boxCollider.enabled = true;

        //set direction
        

        // float localScaleX = targettedEnemy.transform.localScale.x;
        // if (Mathf.Sign(localScaleX) != _direction) {
        //     localScaleX = -localScaleX;
        //     speed = -speed;
        // }

        // targettedEnemy.transform.localScale = new Vector3(localScaleX, transform.localScale.y,targettedEnemy.transform.localScale.z);
    // }

    // private void Deactivate() {
    //     gameObject.SetActive(false);   
    // }
}
