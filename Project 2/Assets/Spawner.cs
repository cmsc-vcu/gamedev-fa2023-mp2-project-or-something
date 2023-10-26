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
                    spawnPoint = new Vector3(Random.Range(40, 45), Random.Range(40, 45), 0);
                    break;
                case 1:
                    spawnPoint = new Vector3(Random.Range(-40, -45), Random.Range(40, 45), 0);
                    break;
                case 2:
                    spawnPoint = new Vector3(Random.Range(40, 45), Random.Range(-40, -45), 0);
                    break;
                case 3:
                    spawnPoint = new Vector3(Random.Range(-40, -45), Random.Range(-40, -45), 0);
                    break;
            }
            GameObject newEnemy = Instantiate(enemy, spawnPoint, Quaternion.identity);
        }
    }

    public static void updateKilled()
    {
        enemiesKilled++;
        if (enemiesLimit < 250)
        {
            if (enemiesLimit < 15)
            {
                enemiesLimit++;
            }
            else if (enemiesLimit < 40)
            {
                enemiesLimit += (enemiesKilled % 2 == 0) ? 1 : 0;
            }
            else if (enemiesLimit < 60)
            {
                enemiesLimit += (enemiesKilled % 3 == 0) ? 2 : 0;
            }
            else
            {
                enemiesLimit += (enemiesKilled % 5 == 0) ? 1 : 0;
            }
        }
    }
}
