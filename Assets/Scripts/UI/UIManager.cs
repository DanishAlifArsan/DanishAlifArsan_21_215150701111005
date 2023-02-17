using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header ("game over")]
    [SerializeField] private GameObject gameOverScreen;
    // [SerializeField] private AudioClip gameOverSound;

    [Header ("pause")]
    [SerializeField] private GameObject pauseScreen;
    // [SerializeField] private AudioClip pauseSound;
    
    void Awake()
    {
        gameOverScreen.SetActive(false);
        // pauseScreen.SetActive(false);
    }

    void Update() {
        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     if(pauseScreen.activeInHierarchy) {
        //         pauseGame(false);
        //     } else {
        //         pauseGame(true);
        //     }  
        // }
    }

    public void gameOver() {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
        // SoundManager.instance.playSound(gameOverSound);
    }

    public void Retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
    }

    public void Quit() {
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void pauseGame(bool status) {
        pauseScreen.SetActive(status);

        if(status) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }
    

}

