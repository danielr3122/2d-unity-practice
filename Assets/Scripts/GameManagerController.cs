using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagerController : MonoBehaviour
{
    private bool isGameStarted;

    private float spawnTime;
    private float time;
    private int waveNumber;
    private int numOfEnemiesAlive;
    private float spawnBoundaryX = 9.39f;
    private float spawnBoundaryY = 5.5f;

    public GameObject enemyPrefab;

    public GameObject startGameScreen;
    public GameObject gameOverScreen;
    public GameObject pauseScreen;
    public GameObject inGameScreen;

    public TextMeshProUGUI waveCountText;
    public TextMeshProUGUI highestWaveText;

    public TestScriptableObject savedStats;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = 2;
        time = 0;
        waveNumber = 1;
        numOfEnemiesAlive = 0;
        isGameStarted = false;
        Time.timeScale = 0;
        startGameScreen.SetActive(true);
        highestWaveText.text = "Highest Wave: " + savedStats.highWave;
        gameOverScreen.SetActive(false);
    }

    public void StartGame(){
        startGameScreen.SetActive(false);
        Cursor.visible = false;
        isGameStarted = true;
        Time.timeScale = 1;
        inGameScreen.SetActive(true);
    }

    public void GameOver(){
        if(waveNumber > savedStats.highWave){
            savedStats.highWave = waveNumber;
        }
        gameOverScreen.SetActive(true);
        Cursor.visible = true;
        isGameStarted = false;
        Time.timeScale = 0;
    }

    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void PauseGame(){
        if(Time.timeScale == 0){
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        } else {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        numOfEnemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if(isGameStarted){
            if(Input.GetKeyDown(KeyCode.P)){
                PauseGame();
            }

            if(numOfEnemiesAlive <= 0){
                Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
                waveNumber++;
                waveCountText.text = "Wave: " + waveNumber;
                Debug.Log("Wave: " + waveNumber);
            } else {
                if(time > spawnTime && numOfEnemiesAlive < waveNumber){
                    time = 0;
                    Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
                }
            }
        }
    }

    Vector2 GenerateSpawnPosition(){
        int spawnSide = Random.Range(0, 2);
        float randomX, randomY;

        // spawnSide = 0 -> spawn on top or bottom of screen
        // spawnSide = 1 -> spawn on left or right of screen
        if(spawnSide == 0){
            randomY = (Random.Range(0, 2) * 2 - 1) * spawnBoundaryY;
            randomX = Random.Range(-spawnBoundaryX, spawnBoundaryX);
        } else {
            randomX = (Random.Range(0, 2) * 2 - 1) * spawnBoundaryX;
            randomY = Random.Range(-spawnBoundaryY, spawnBoundaryY);
        }
        return new Vector2(randomX, randomY);
    }
}
