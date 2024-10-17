using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using System;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    private int wavecount = 1;

    //public TMP_Text scoreText;
    //public TMP_Text restartText;
    //public TMP_Text gameOverText;

    private bool gameOver;
    private bool restart;
    private int score;

    void Start()
    {
        gameOver = false;
        restart = false;
        //restartText.text = "";
        //gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    int getScore() 
    { 
        return score; 
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            switch(wavecount)
            {
                case 1:
                    for (int i = 0; i < hazardCount; i++)
                    {
                        GameObject hazard = hazards[UnityEngine.Random.Range(0, hazards.Length)];
                        Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(hazard, spawnPosition, spawnRotation);
                        yield return new WaitForSeconds(spawnWait);
                        if(score >= 200)
                        {
                            wavecount = 2;
                        }
                    }
                    break;
                case 2:
                    if (score >= 400)
                    {
                        wavecount = 3;
                    }
                    break;
                case 3:
                    if (score >= 800)
                    {
                        Debug.Log("YOU WIN");
                    }
                    break;
            }
            //for (int i = 0; i < hazardCount; i++)
            //{
            //    GameObject hazard = hazards[Random.Range(0, hazards.Length)];
            //    Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
            //    Quaternion spawnRotation = Quaternion.identity;
            //    Instantiate(hazard, spawnPosition, spawnRotation);
            //    yield return new WaitForSeconds(spawnWait);
            //}
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                //restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        //scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        //gameOverText.text = "Game Over!";
        gameOver = true;
    }
}