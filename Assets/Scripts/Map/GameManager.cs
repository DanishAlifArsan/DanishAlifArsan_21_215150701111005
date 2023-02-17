using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TowerButton TowerBtn { get; set; }

    [Header ("default settings")]
    [SerializeField] private int startingCurrency;
    [SerializeField] private int startingPlayerHealth;
    public float StartingPlayerHealth { 
        get {
            return startingPlayerHealth;
        }
    }
    public float CurrentPlayerHealth {get; set;}

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

    private int wave = 0;

    [SerializeField] private Text waveText;

    private bool waveEnd = true;

    private Tower tower;

    [Header ("game over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Text highscoreText;
    // [SerializeField] private AudioClip gameOverSound;

    [Header ("pause")]
    [SerializeField] private GameObject pauseScreen;
    // [SerializeField] private AudioClip pauseSound;

    // Start is called before the first frame update
    private void Start()
    {
        CurrentPlayerHealth = StartingPlayerHealth;
        EnemyPool = GetComponent<EnemyController>();
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
        
        HandleEscape();

        if (CurrentPlayerHealth <= 0) 
        {
            gameOver();
        }
    }

    public void PickTower(TowerButton towerBtn) {
        if (Currency >= towerBtn.Cost)
        {
            this.TowerBtn = towerBtn;
            TowerGrab.FindObjectOfType<TowerGrab>().Activate(towerBtn.Sprite);
        }
    }

    public void SelectTower(Tower tower) {
        if(this.tower != null) {
            this.tower.Select();
        }
        
        this.tower = tower;
        this.tower.Select();
    }

    public void DeselectTower() {
        if(this.tower != null) {
            this.tower.Select();
        }
        this.tower = null;
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
    }

    public void StartWave() {
        waveEnd = false;

        wave++;

        waveText.text = string.Format("Wave : <color=cyan>{0}</color>", wave);

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

            yield return new WaitForSeconds(2.5f);
        }
        yield return new WaitForSeconds(10f*(wave-1));

        waveEnd = true;
    }

    public void gameOver() {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
        highscoreText.text = waveText.text;
        // SoundManager.instance.playSound(gameOverSound);
    }

    // public void Play() {
    //     SceneManager.LoadScene(1);
    // }

    public void Retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    // public void Quit() {
    //     Application.Quit();
    // }

    public void pauseGame(bool status) {
        pauseScreen.SetActive(status);

        if(status) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }

    private void DropTower() {
        TowerBtn = null;
        TowerGrab.FindObjectOfType<TowerGrab>().Deactivate();
    }

}
