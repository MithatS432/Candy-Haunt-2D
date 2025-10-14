using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;
    public bool SpawnEnemies(bool value)
    {
        if (value)
        {
            while (!PlayerControl.isAlive)
            {
                int randomIndex = Random.Range(0, enemyPrefabs.Length);
                int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
                Instantiate(enemyPrefabs[randomIndex], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
                return false;
            }
        }
        return true;
    }
}
