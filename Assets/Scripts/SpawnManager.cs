using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;
    public float spawnInterval = 5f;
    private bool spawningActive = false;

    private void Start()
    {
        
    }

    public void StartSpawning()
    {
        if (!spawningActive)
        {
            spawningActive = true;
            StartCoroutine(SpawnLoop());
        }
    }

    private IEnumerator SpawnLoop()
    {
        while (PlayerControl.isAlive)
        {
            PlayerControl player = FindAnyObjectByType<PlayerControl>();
            if (player != null && player.GetHouseCount() <= 0)
            {
                spawningActive = false;
                yield break;
            }

            int randomEnemy = Random.Range(0, enemyPrefabs.Length);
            int randomSpawn = Random.Range(0, spawnPoints.Length);
            Instantiate(enemyPrefabs[randomEnemy], spawnPoints[randomSpawn].position, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }

        spawningActive = false;
    }
}
