using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float randomX = Random.Range(-10f, 10f);
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < -5.25f) {
            transform.position = new Vector3(randomX, 7f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player") {
            Destroy(this.gameObject);
            Player player = other.transform.GetComponent<Player>();
            if(player != null) {
                player.damage();
            }
        }

        if (other.tag == "Laser") {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
