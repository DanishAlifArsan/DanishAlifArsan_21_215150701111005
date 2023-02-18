using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private GameObject[] projectilePool;

    public GameObject GetObject(string type) {
        for (int i = 0; i < projectilePool.Length; i++)
        {
            if (projectilePool[i].name == type)
            {
                GameObject newObj = Instantiate(projectilePool[i]);
                newObj.name = type;
                return newObj;
            }
        }
        return null;
    }
}
