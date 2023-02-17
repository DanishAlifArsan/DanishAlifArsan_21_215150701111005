// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;

// public class UIManager : MonoBehaviour
// {
//     [Header ("game over")]
//     [SerializeField] private GameObject gameOverScreen;
//     [SerializeField] private Text highscoreText;
//     // [SerializeField] private AudioClip gameOverSound;

//     [Header ("pause")]
//     [SerializeField] private GameObject pauseScreen;
//     // [SerializeField] private AudioClip pauseSound;
    
//     void Awake()
//     {
//         gameOverScreen.SetActive(false);
//         pauseScreen.SetActive(false);
//     }

//     void Update() {
//         if (Input.GetKeyDown(KeyCode.Escape))
//         {
//             if(pauseScreen.activeInHierarchy) {
//                 pauseGame(false);
//             } else {
//                 pauseGame(true);
//             }  
//         }
//     }

//     public void gameOver(int highscore) {
//         gameOverScreen.SetActive(true);
//         Time.timeScale = 0;
//         highscoreText.text = string.Format("Wave : <color=cyan>{0}</color>", highscore);
//         // SoundManager.instance.playSound(gameOverSound);
//     }

//     public void Play() {
//         SceneManager.LoadScene(1);
//     }

//     public void Retry() {
//         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
//         Time.timeScale = 1;
//     }

//     public void MainMenu() {
//         SceneManager.LoadScene(0);
//         Time.timeScale = 1;
//     }

//     public void Quit() {
//         Application.Quit();
//     }

//     public void pauseGame(bool status) {
//         pauseScreen.SetActive(status);

//         if(status) {
//             Time.timeScale = 0;
//         } else {
//             Time.timeScale = 1;
//         }
//     }
    

// }

