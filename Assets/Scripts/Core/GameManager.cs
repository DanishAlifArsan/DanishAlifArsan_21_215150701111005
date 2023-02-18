using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header ("default settings")]
    [SerializeField] private int startingCurrency;
    [SerializeField] private int startingPlayerHealth;

    [Header ("sound")]
    [SerializeField] private AudioClip waveSound;
    [SerializeField] private AudioClip buySound;
    [SerializeField] private AudioClip selectSound;
    [SerializeField] private AudioClip successSound;
    [SerializeField] private AudioClip failedSound;

    [Header ("UI")]
    [SerializeField] private Text waveText;
    [SerializeField] private GameObject sellButton;
    [SerializeField] private GameObject statPanel;
    [SerializeField] private Text statText;
    [SerializeField] private Text currencyText;
    
    [Header ("game over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Text highscoreText;

    [Header ("pause")]
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private AudioClip pauseSound;

    public TowerButton TowerBtn { get; set; }
    
    public float StartingPlayerHealth { 
        get {
            return startingPlayerHealth;
        }
    }

    public float CurrentPlayerHealth {get; set;}

    private int currency;

    public EnemyController EnemyPool { get; set; }
    public ProjectileController ProjectilePool { get; set; }

    public int Currency { 
        get {
            return currency;
        } 
        set {
            this.currency = value;
            this.currencyText.text = value.ToString() + " $";
        } 
    }

    private int wave = 0;

    private bool waveEnd = true;

    private Tower tower;

    private float spawnTime = 2.5f;

    // Start is called before the first frame update
    private void Start()
    {
        CurrentPlayerHealth = StartingPlayerHealth;
        EnemyPool = GetComponent<EnemyController>();
        ProjectilePool = GetComponent<ProjectileController>();
        Currency = startingCurrency;

        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (waveEnd)
        {
            StartWave();
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (this.tower == null && !TowerGrab.FindObjectOfType<TowerGrab>().IsVisible)
            {
                if(pauseScreen.activeInHierarchy) {
                    pauseGame(false);
                } else {
                    pauseGame(true);
                } 
            }
            else if (TowerGrab.FindObjectOfType<TowerGrab>().IsVisible)
            {
                DropTower();
            } else if (this.tower != null) {
                DeselectTower();
            }
        }

        if (CurrentPlayerHealth <= 0) 
        {
            gameOver();
        }
    }

    //tower related

    public void PickTower(TowerButton towerBtn) {
        if (Currency >= towerBtn.Cost)
        {
            this.TowerBtn = towerBtn;
            TowerGrab.FindObjectOfType<TowerGrab>().Activate(towerBtn.Sprite);
            SoundManager.Instance.PlaySound(successSound);
        } else {
            SoundManager.Instance.PlaySound(failedSound);
        }
    }

    private void DropTower() {
        TowerBtn = null;
        TowerGrab.FindObjectOfType<TowerGrab>().Deactivate();
    }

    public void SelectTower(Tower tower) {
        if(this.tower != null) {
            this.tower.Select();
        }
        SoundManager.Instance.PlaySound(selectSound);
        
        this.tower = tower;
        this.tower.Select();

        sellButton.SetActive(true);
    }

    public void DeselectTower() {
        if(this.tower != null) {
            this.tower.Select();
        }
        this.tower = null;
        sellButton.SetActive(false);
    }

    public void BuyTower() {
        if (Currency >= TowerBtn.Cost) {
            Currency -= TowerBtn.Cost;
            TowerGrab.FindObjectOfType<TowerGrab>().Deactivate();
            SoundManager.Instance.PlaySound(buySound);
        }
    }

    public void SellTower() {
        if (this.tower != null)
        {
            SoundManager.Instance.PlaySound(buySound);

            Currency += this.tower.Cost / 2;

            this.tower.GetComponentInParent<TileScript>().IsEmpty = true;

            Destroy(this.tower.transform.parent.gameObject);

            DeselectTower();
        }
    }

    //tower stats

    public void TowerStats() {
        statPanel.SetActive(true);
    }
    public void HideTowerStats() {
        statPanel.SetActive(false);
    }

    public void SetTowerStats(string text) {
        statText.text = text;
    }

    //wave related

    public void StartWave() {
        waveEnd = false;

        wave++;

        waveText.text = string.Format("Wave : <color=cyan>{0}</color>", wave);

        SoundManager.Instance.PlaySound(waveSound);

        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave() { 
        for (int i = 0; i < (wave * 2) + 1; i++)
        {
            LevelManager.FindObjectOfType<LevelManager>().GeneratePath();

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

            Enemy enemy = EnemyPool.GetObject(type).GetComponent<Enemy>();
        
            enemy.Spawn();

            if (wave % 5 == 0) 
            {
                spawnTime /= wave / 5;
            }

            yield return new WaitForSeconds(spawnTime);
        }
        yield return new WaitForSeconds(10f*Mathf.Abs(wave-spawnTime));

        waveEnd = true;
    }

    //menu screen

    public void gameOver() {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
        highscoreText.text = waveText.text;
    }

    public void Retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void pauseGame(bool status) {
        pauseScreen.SetActive(status);
        SoundManager.Instance.PlaySound(pauseSound);

        if(status) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }

}
