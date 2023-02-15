﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    [SerializeField] private GameObject tower;

    [SerializeField] private Sprite sprite;
    [SerializeField] private int cost;
    [SerializeField] private Text costText;

    public GameObject Tower { 
        get {return tower;} 
    }

     public Sprite Sprite { 
        get {return sprite;} 
    }

     public int Cost { 
        get {return cost;} 
    }
    

    // Start is called before the first frame update
    private void Start()
    {
        costText.text = cost + "$";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
