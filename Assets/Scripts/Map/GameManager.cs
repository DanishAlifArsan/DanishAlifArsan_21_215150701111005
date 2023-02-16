using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public TowerButton TowerBtn { get; set; }

    private int currency;

    [SerializeField] private Text currencyText;

    public EnemyController EnemyPool { get; set; }

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
    private void Start()
    {
        EnemyPool = GetComponent<EnemyController>();
        Currency = 5;
    }

    // Update is called once per frame
    private void Update()
    {
        HandleEscape();
    }

    public void PickTower(TowerButton towerBtn) {
        if (Currency >= towerBtn.Cost)
        {
            this.TowerBtn = towerBtn;
            TowerGrab.FindObjectOfType<TowerGrab>().Activate(towerBtn.Sprite);
        }
    }

    public void BuyTower() {
        if (Currency >= TowerBtn.Cost) {
            Currency -= TowerBtn.Cost;
            TowerGrab.FindObjectOfType<TowerGrab>().Deactivate();
        }
    }

    private void HandleEscape() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TowerGrab.FindObjectOfType<TowerGrab>().Deactivate();
        }
    }

    public void StartWave() {
         StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave() {   
        int enemyIndex = Random.Range(0,4);

        string type = string.Empty;

        switch (enemyIndex)
        {
            case 0:
                type = "Enemy 1";
                break;
            case 1:
                type = "Enemy 2";
                break;
            case 2:
                type = "Enemy 3";
                break;
            case 3:
                type = "Enemy 4";
                break;
        }

        EnemyPool.GetObject(type).GetComponent<Enemy>();

        yield return new WaitForSeconds(2.5f);
    }
}
