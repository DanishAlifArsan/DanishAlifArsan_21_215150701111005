using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float shotCooldown;

    private float cooldownTimer = Mathf.Infinity;

    [SerializeField] private GameObject[] projectile;

    private SpriteRenderer spriteRend;
    private Enemy targettedEnemy;
    private Queue<Enemy> enemyQueue = new Queue<Enemy>();

    // Start is called before the first frame update
    private void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
         if(cooldownTimer > shotCooldown) {
            Attack();
        }
        
        Debug.Log(targettedEnemy);
    }

    public void Select() {
        spriteRend.enabled = !spriteRend.enabled;
    }

    private void OnTriggerEnter2D(Collider2D Collision) {
        if (Collision.tag == "Enemy") {
            targettedEnemy = Collision.GetComponent<Enemy>();
            enemyQueue.Enqueue(Collision.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit2D(Collider2D Collision) {
        if (Collision.tag == "Enemy") {
            targettedEnemy = null;
        }
    }

    private void Attack() {
        if (targettedEnemy != null && enemyQueue.Count > 0)
        {
            Debug.Log("attacks");
            targettedEnemy = enemyQueue.Dequeue();

            projectile[findProjectile()].transform.position = transform.position;
            projectile[findProjectile()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
        }
    }

    private int findProjectile() {
        for (int i = 0; i < projectile.Length; i++)
        {
            if(!projectile[i].activeInHierarchy) {
                return i;
            }
        }
        
        return 0;
    }
}
