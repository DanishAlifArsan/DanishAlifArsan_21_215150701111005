using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //following video
    [SerializeField] private string projectileType;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float shotCooldown;

    public float ProjectileSpeed { 
        get {return projectileSpeed;}  
    }

    private bool canAttack = true;

    private float cooldownTimer = Mathf.Infinity;
    private SpriteRenderer spriteRend;
    public Queue<Enemy> enemyQueue = new Queue<Enemy>();

    private Enemy targettedEnemy;
    public Enemy TargettedEnemy { get; set; }
    
    public int Cost { get; set; }

    // Start is called before the first frame update
    private void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!canAttack)
        {
            cooldownTimer += Time.deltaTime;

            if (cooldownTimer >= shotCooldown)
            {
                canAttack = true;
                cooldownTimer = 0;
            }
        }
        if (TargettedEnemy != null && enemyQueue.Count > 0)
        {
            TargettedEnemy = enemyQueue.Dequeue();
        }
        if (TargettedEnemy != null && TargettedEnemy.gameObject.activeInHierarchy)
        {
            if (canAttack)
            {
                Shoot(); 
                canAttack = false;
            }
        }
    }

    private void Shoot() {
        Projectile projectile = GameManager.FindObjectOfType<GameManager>().ProjectilePool.GetObject(projectileType).GetComponent<Projectile>();
        projectile.transform.position = transform.position;

        projectile.Initialize(this);
    }

    public void Select() {
        spriteRend.enabled = !spriteRend.enabled;
    }

    private void OnTriggerEnter2D(Collider2D Collision) {
        if (Collision.tag == "Enemy") {
            TargettedEnemy = Collision.GetComponent<Enemy>();
            enemyQueue.Enqueue(Collision.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit2D(Collider2D Collision) {
        if (Collision.tag == "Enemy") {
            TargettedEnemy = null;
        }
    }
}
