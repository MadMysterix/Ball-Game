using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject hazard1;
    public GameObject hazard2;

    public Vector3 spawnValues1;
    public Vector3 spawnValues2;
    public float cubeSizeMin;
    public float cubeSizeMax;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public GUIText scoreText;
    private float score;
    public GUIText gameOverText;
    public GUIText restartText;
    private bool gameOver;
    private bool restart;


    void Start()
    {
        gameOver = false;
        restart = false;
        gameOverText.text = "";
        restartText.text = "";
        score = 0.0f;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }
    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadSceneAsync("Main");
            }
        }
    }
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition1 = new Vector3(spawnValues1.x, spawnValues1.y, spawnValues1.z);
                Quaternion spawnRotation1 = Quaternion.identity;
                Instantiate(hazard1, spawnPosition1, spawnRotation1);
                float range = Random.Range(cubeSizeMin, cubeSizeMax);
                Vector3 randomSize = new Vector3(range, 1, 0.5f);
                hazard1.transform.localScale = randomSize;
                Vector3 spawnPosition2 = new Vector3(spawnValues2.x, spawnValues2.y, spawnValues2.z);
                Quaternion spawnRotation2 = Quaternion.identity;
                Instantiate(hazard2, spawnPosition2, spawnRotation2);
                hazard2.transform.localScale = new Vector3(12 - range, 1, 0.5f);
                yield return new WaitForSeconds(spawnWait);

            }
            yield return new WaitForSeconds(waveWait);
            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }
    public void AddScore(float newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}
