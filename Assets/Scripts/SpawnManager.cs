using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject tract;
    public GameObject leftBird;
    public GameObject RightBird;

    private const float startMaxTimeSpawnBird = 3.5f;
    private const float startMinTimeSpawnBird = 1f;
    private float maxTimeSpawnBird = 3.5f;
    private float minTimeSpawnBird = 1f;

    private float posAbovePlayer = 1.2f;
    private float spawnPosXBird = 3.7f;
    private float spawnPosYBird = 2f;

    private void Start()
    {
        StartCoroutine(SpawnLeftBird());
        StartCoroutine(SpawnRightBird());
    }

    public void UpdateTimeSpawn(float complexity) 
    {
        if (complexity != 1)
        {
            minTimeSpawnBird = startMinTimeSpawnBird / complexity;
            maxTimeSpawnBird = startMaxTimeSpawnBird / complexity;
        }
        else 
        {
            minTimeSpawnBird = startMaxTimeSpawnBird;
            maxTimeSpawnBird = startMaxTimeSpawnBird;
        }
    }


    public void SpawnTract(Vector2 posPlayer) 
    {
        Instantiate(tract,new Vector2(posPlayer.x, posPlayer.y + posAbovePlayer),Quaternion.identity);     
    }

    IEnumerator SpawnLeftBird() 
    {
        while (true)
        {
                Instantiate(leftBird, new Vector2(-spawnPosXBird, spawnPosYBird), Quaternion.identity);
                yield return new WaitForSeconds(
                                           Random.Range(minTimeSpawnBird, maxTimeSpawnBird));          
        }
    }

    IEnumerator SpawnRightBird()
    {
        while (true)
        {      
                Instantiate(RightBird, new Vector2(spawnPosXBird, spawnPosYBird), Quaternion.identity);
                yield return new WaitForSeconds(
                                           Random.Range(minTimeSpawnBird, maxTimeSpawnBird));           
        }
    }
}
