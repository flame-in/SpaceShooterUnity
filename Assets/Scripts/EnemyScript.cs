using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 2.5f;

    PlayerScripts playerScripts;
    // Start is called before the first frame update
    void Start()
    {
        playerScripts = GameObject.Find("Player").GetComponent<PlayerScripts>();
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();
    }

    //Movement
    void calculateMovement()
    {
        transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime);
        if (transform.position.y < -7f)
        {
            float randomX = Random.Range(-8.5f, 8.5f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    //Collisions
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            if (playerScripts != null)
            {
                playerScripts.addScore(10);
            }
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }

        if (other.tag == "Player")
        {
            if(playerScripts != null)
            {
                playerScripts.DamagePlayer();
                playerScripts.addScore(10);
            }
            Destroy(this.gameObject);
        }
    }
}
