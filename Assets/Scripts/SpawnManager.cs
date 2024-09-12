using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    private float spawnRange = 9;
    private int spawnDelay = 9;
    private int totalEnemies;
    private int waveNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Debug.Log("Starting...");
        SpawnEnemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        if(FindEnemies() == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);            
            
        }
    }
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        UnityEngine.Debug.Log("Attempting to spawn " + enemiesToSpawn + " enemies.");
        if(FindPowerups() < 1)
        {
            StartCoroutine(powerupSpawnDelayRoutine(UnityEngine.Random.Range(3, spawnDelay)));
        }
        else
        {
            Destroy(locatePowerup());
            StartCoroutine(powerupSpawnDelayRoutine(UnityEngine.Random.Range(3, spawnDelay)));
        }
        for(int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEntity("enemy");
        }
    }
    void SpawnEntity(String spawnType)//Spawn an entity based on string input
    {
        switch(spawnType)
        {
            case "enemy"://If the input is 'enemy', spawn an enemy
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
            break;
            case "powerup"://If the input is 'powerup', spawn a powerup
            Instantiate(powerupPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
            break;
        }
    }
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = UnityEngine.Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = UnityEngine.Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, -0.5f, spawnPosZ);
        return randomPos;
    }
    int FindEnemies()//Find how many enemies are in the scene
    {
        int enemyCount = FindObjectsOfType<EnemyController>().Length;
        return enemyCount;
    }
    int FindPowerups()//Find how many powerups are in the scene
    {
        GameObject[] count = GameObject.FindGameObjectsWithTag("Powerup");//Create an array based on objects found with the tag 'powerup'
        int powerupCount = count.Length;//Create an integer with the length of objects found in the array
        return powerupCount;
    }
    GameObject locatePowerup()
    {
        GameObject[] foundPowerup = GameObject.FindGameObjectsWithTag("Powerup");
        return foundPowerup[0];
    }
    IEnumerator powerupSpawnDelayRoutine(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);        
        SpawnEntity("powerup");
    }
}
