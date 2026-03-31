using UnityEngine;
using System.Collections;
using TMPro;
public class waveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;

    public float spawnTime = 5.5f;
    private float countdown = 2f;
    private int waveNumber = 0;

    public TMP_Text waveCountdownText;
    void Update()
    {
        
        if(countdown <= 0f)
        {
            StartCoroutine(spawnWave());
            countdown = spawnTime;
        }
        countdown -= Time.deltaTime;

        waveCountdownText.text = Mathf.Round(countdown).ToString();
    }
    IEnumerator spawnWave()
    {
        waveNumber++;
        for (int i = 0; i < waveNumber; i++)
        {
            spawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
        
        //numOfEnemies = waves[waveNumber].count;
    }
    void spawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
