using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]    private float _moveSpeed = 2.5f;
    private PlayerScripts playerScripts;
    private Animator _animator;

    [SerializeField]    private AudioSource _audioSource;
    [SerializeField]    private AudioClip _explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        playerScripts = GameObject.Find("Player").GetComponent<PlayerScripts>();

        if(playerScripts == null)
        {
            Debug.LogError("Player script is null");
        }

        _animator = GetComponent<Animator>();

        if(_animator == null)
        {
            Debug.LogError("Animator component is null");
        }

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("The audioSource is null.");
        }
        else
        {
            _audioSource.clip = _explosionSound;
        }
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
            _animator.SetTrigger("OnEnemyDeath");
            _moveSpeed = 1f;
            Destroy(this.gameObject, 2f);
            Destroy(other.gameObject);
            _audioSource.Play();
        }

        if (other.tag == "Player")
        {
            if(playerScripts != null)
            {
                playerScripts.DamagePlayer();
                playerScripts.addScore(10);
            }
            _animator.SetTrigger("OnEnemyDeath");
            _moveSpeed = 1f;
            Destroy(this.gameObject, 2f);
            _audioSource.Play();
        }
    }
}
