using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private GameObject _tripleShot;
    [SerializeField]
    private float _offset = 1.05f;
    private float _canFire = -1f;
    public float _fireRate = 0.5f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleShotActive = false;
    private IEnumerator coroutine;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if(_laser == null) {
            Debug.LogError("Laser is null");
        }
        if(_spawnManager == null) {
            Debug.LogError("Spawn manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire) {
            shoot();
        }
    }

    void shoot() {
            if(_isTripleShotActive == true) {
                Instantiate(_tripleShot, transform.position, Quaternion.identity);
            } else {
                Instantiate(_laser, transform.position + new Vector3(0, _offset, 0), Quaternion.identity);
            }
            _canFire = Time.time + _fireRate;
    }

    void calculateMovement() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if(transform.position.x >= 11.3f) {
            transform.position = new Vector3(-11.3f, transform.position.y, transform.position.z);
        } else if(transform.position.x <= -11.3f) {
            transform.position = new Vector3(11.3f, transform.position.y, transform.position.z);
        }
    }

    public void damage() {
        _lives -= 1;

        if(_lives == 0) {
            _spawnManager.onPlayerDeath();
            Destroy(this.gameObject);
        }
    }
    IEnumerator tripleShotPowerDownRoutine(float duration) {
            yield return new WaitForSeconds(duration);
            _isTripleShotActive = false;
    }
    public void onCollected() {
        coroutine = tripleShotPowerDownRoutine(5f);
        StartCoroutine(coroutine);
        _isTripleShotActive = true;
    }
}
