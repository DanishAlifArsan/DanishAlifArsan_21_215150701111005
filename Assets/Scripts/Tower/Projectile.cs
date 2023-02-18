using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private AudioClip projectileSound;

    private BoxCollider2D boxCollider;

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
    }

    // Update is called once per frame
    private void Update()
    {
        if (target != null && target.gameObject.activeInHierarchy)
        {
            SoundManager.Instance.PlaySound(projectileSound);

            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * towerParent.ProjectileSpeed);
            Vector2 direction = target.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        } else if (target == null) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if(collision.tag == "Enemy") {
            Debug.Log("hit enemy");
            
            Destroy(gameObject);
            collision.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
