﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public TowerButton TowerBtn { get; set; }

    private int currency;

    [SerializeField] private Text currencyText;

    public int Currency { 
        get {
            return currency;
        } 
        set {
            this.currency = value;
            this.currencyText.text = value.ToString() + " $";
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        Currency = 5;
    }

    // Update is called once per frame
    private void Update()
    {
        HandleEscape();
    }

    public void PickTower(TowerButton towerBtn) {
        this.TowerBtn = towerBtn;
        TowerGrab.FindObjectOfType<TowerGrab>().Activate(TowerBtn.Sprite);
    }

    public void BuyTower() {
        TowerGrab.FindObjectOfType<TowerGrab>().Deactivate();
    }

    private void HandleEscape() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TowerGrab.FindObjectOfType<TowerGrab>().Deactivate();
        }
    }
}
