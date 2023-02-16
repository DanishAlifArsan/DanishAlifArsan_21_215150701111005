using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;

    private Stack<Node> path;

    public Grid GridPosition { get; set; }

    private Vector3 destination;
    // Start is called before the first frame update
    void Start()
    {
        
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
            Destroy(gameObject);
        }
    }
}
