﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float shotCooldown;

    private float cooldownTimer = Mathf.Infinity;

    [SerializeField] private GameObject[] projectile;

    private SpriteRenderer spriteRend;
    // private Enemy targettedEnemy;
    public Queue<Enemy> enemyQueue = new Queue<Enemy>();

    private Enemy targettedEnemy;

    public Enemy TargettedEnemy { 
        get; 
        set; }

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
        
        Debug.Log(TargettedEnemy);
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

    private void Attack() {
        if (TargettedEnemy != null && enemyQueue.Count > 0)
        {
            Debug.Log("attacks");
            TargettedEnemy = enemyQueue.Dequeue();

            projectile[findProjectile()].transform.position = transform.position;
            projectile[findProjectile()].GetComponent<Projectile>().SetVisibility();
            projectile[findProjectile()].GetComponent<Projectile>().Initialize(this);
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
