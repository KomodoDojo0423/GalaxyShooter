using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _tripleShot;
    [SerializeField]
    private GameObject _speedPowerup;
    [SerializeField]
    private GameObject _powerupContainer;

    private bool _stopSpawning;
    
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
        StartCoroutine(SpawnSpeedPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(3.0f);
            GameObject newEnemy = Instantiate(_enemy, new Vector3 (Random.Range(-8.5f, 8.5f), 7.5f, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
        }
        
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(Random.Range(3.0f, 8.0f));
            GameObject newPowerup = Instantiate(_tripleShot, new Vector3(Random.Range(-8.5f, 8.5f), 7.5f, 0), Quaternion.identity);
            newPowerup.transform.parent = _powerupContainer.transform;
        }
    }

    IEnumerator SpawnSpeedPowerupRoutine()
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(Random.Range(5.0f, 11.0f));
            GameObject speedPowerup = Instantiate(_speedPowerup, new Vector3(Random.Range(-8.5f, 8.5f), 7.5f, 0), Quaternion.identity);
            speedPowerup.transform.parent = _powerupContainer.transform;
        }
        
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

}
