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
    private bool _stopSpawning = false;
    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemyRoutine(3f));
        StartCoroutine(spawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator spawnEnemyRoutine(float waitTime) {
        while(!_stopSpawning) {
            if(_enemy != null) {
                Vector3 posToSpawn = new Vector3(Random.Range(-10f, 10f), 7f , 0f);
                GameObject newEnemy = Instantiate(_enemy, posToSpawn, Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(waitTime);
            }
        }
    }

    IEnumerator spawnPowerUpRoutine() {
        while(!_stopSpawning) {
            if(_tripleShot != null) {
                Vector2 posToSpawn = new Vector2(Random.Range(-10f,10f), 7f);
                Instantiate(_tripleShot, posToSpawn, Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(3f, 7f));
            }
        }
    }

    public void onPlayerDeath() {
        _stopSpawning = true;
    }
}
