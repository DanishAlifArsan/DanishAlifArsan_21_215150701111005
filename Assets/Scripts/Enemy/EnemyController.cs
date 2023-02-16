using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPool;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetObject(string type) {
        for (int i = 0; i < enemyPool.Length; i++)
        {
            if (enemyPool[i].name == type)
            {
                GameObject newObj = Instantiate(enemyPool[i]);
                newObj.name = type;
                return newObj;
            }
        }

        return null;
    }
}
