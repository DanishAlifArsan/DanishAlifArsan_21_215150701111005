using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header ("Stats")]
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private int killReward;
    [SerializeField] private float startingHealth;

    [Header ("Audio")]
    [SerializeField] private AudioClip spawnSound;
    [SerializeField] private AudioClip exitSound;
    [SerializeField] private AudioClip deathSound;

    public float currentHealth {get; private set;}

    private Stack<Node> path;

    private Animator anim;
    private bool dead;

    public Grid GridPosition { get; set; }

    private Vector3 destination;

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,destination,speed * Time.deltaTime);

        if (transform.position == destination)
        {
            if(path != null && path.Count > 0) {
                GridPosition = path.Peek().GridPosition;
                destination = path.Pop().WorldPosition;
            }
        }
    }

    public void Spawn() {
        transform.position = LevelManager.FindObjectOfType<LevelManager>().SpawnPoint.transform.position;

        SoundManager.Instance.PlaySound(spawnSound);

        SetPath(LevelManager.FindObjectOfType<LevelManager>().Path);
    }

    private void SetPath(Stack<Node> newPath) {
        if (newPath != null)
        {
            this.path = newPath;
            GridPosition = path.Peek().GridPosition;
            destination = path.Pop().WorldPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D Collision) {
        if (Collision.tag == "ExitPoint") {
            GameManager.FindObjectOfType<GameManager>().CurrentPlayerHealth -= damage;
            SoundManager.Instance.PlaySound(exitSound);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float _damage) {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        
        if (currentHealth > 0) {
            anim.SetTrigger("hurt");
        }
        else {
            if(!dead) {
                anim.SetTrigger("die");
                dead = true;
                GameManager.FindObjectOfType<GameManager>().Currency += killReward;
                SoundManager.Instance.PlaySound(deathSound);
            } 
        }
    }

    private void Death(){
        Destroy(gameObject);
    }
}
