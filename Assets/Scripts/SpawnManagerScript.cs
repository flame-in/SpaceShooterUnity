using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]    private GameObject _enemyPrefab;
    [SerializeField]    private GameObject[] _powerUpPrefab;
    [SerializeField]    private GameObject _enemyContainer;

    private bool _stopSpawning = false;

    public void startSpawning()
    {
        StartCoroutine(spawnEnemyRoutine());
        StartCoroutine(spawnPowerUpRoutine());
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (_stopSpawning == false)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-8.5f, 8.5f), 7f, 0f), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(3.5f);
        }
    }

    IEnumerator spawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (_stopSpawning == false)
        {
            int powerUpId = Random.Range(0, 2);

            GameObject newPowerUp = Instantiate(_powerUpPrefab[Random.Range(0, 3)], new Vector3(Random.Range(-8.5f, 8.5f), 7f, 0f), Quaternion.identity); ;
            newPowerUp.transform.parent = _enemyContainer.transform;
           
            
            yield return new WaitForSeconds(Random.Range(8f,16f));
        }
        
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
