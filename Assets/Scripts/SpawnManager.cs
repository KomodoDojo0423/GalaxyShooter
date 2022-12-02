using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _enemyContainer;

    private bool _stopSpawning;
    
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(5.0f);
            GameObject newEnemy = Instantiate(_enemy, new Vector3 (Random.Range(-8.5f, 8.5f), 7.5f, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
        }
        
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

}
