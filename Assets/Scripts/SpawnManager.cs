using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _enemyContainer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5.0f);
            GameObject newEnemy = Instantiate(_enemy, new Vector3 (Random.Range(-8.5f, 8.5f), 7.5f, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
        }
        
    }

}
