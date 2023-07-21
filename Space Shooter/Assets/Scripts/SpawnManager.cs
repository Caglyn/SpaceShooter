using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject _tripleShotPowerupPrefab;

    private bool _stopSpawning = false;

    void Start()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    IEnumerator EnemySpawnRoutine()
    {
        while(!_stopSpawning)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.4f, 8.4f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator PowerupSpawnRoutine()
    {
        while(!_stopSpawning)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.4f, 8.4f), 7, 0);
            Instantiate(_tripleShotPowerupPrefab, posToSpawn, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(5, 10));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
