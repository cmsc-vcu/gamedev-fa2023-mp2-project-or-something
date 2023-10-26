using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnRate = 2f;
    private float spawnTimer = 0f;
    public static int enemiesKilled = 0;
    public static int enemiesLimit = 1;
    private Vector3 spawnPoint;

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < enemiesLimit)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    spawnPoint = new Vector3(Random.Range(40, 50), Random.Range(40, 50), 0);
                    break;
                case 1:
                    spawnPoint = new Vector3(Random.Range(-40, -50), Random.Range(40, 50), 0);
                    break;
                case 2:
                    spawnPoint = new Vector3(Random.Range(40, 50), Random.Range(-40, -50), 0);
                    break;
                case 3:
                    spawnPoint = new Vector3(Random.Range(-40, -50), Random.Range(-40, -50), 0);
                    break;
            }
            GameObject newEnemy = Instantiate(enemy, spawnPoint, Quaternion.identity);
        }
    }

    public static void updateKilled()
    {
        enemiesKilled++;
        if (enemiesLimit < 100)
        {
            enemiesLimit++;
        }
    }
}
