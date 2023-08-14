using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    [SerializeField]    private float _speed = 3f;
    [SerializeField]    private AudioClip _powerUpPickup;
    //PowerUP IDs
    // 0 - TripleShot
    // 1 - Speed
    // 2 - Shields

    [SerializeField]    private int powerUpId;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < -7f)
        {
            Destroy(this.gameObject);
        }
    }

    // On collision with player - add power up
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerScripts playerScripts = collision.GetComponent<PlayerScripts>();
            if(playerScripts != null)
            {
                AudioSource.PlayClipAtPoint(_powerUpPickup, Camera.main.transform.position);
                switch (powerUpId)
                {
                    case 0: 
                        playerScripts.EnableTripleShot();
                        Debug.Log("Enabled Tripleshot powerup.");
                        break;
                    case 1:
                        playerScripts.EnableSpeedBoost();
                        Debug.Log("Enabled Speed powerup.");
                        break;
                    case 2:
                        playerScripts.EnableShields();
                        Debug.Log("Enabled Shields powerup.");
                        break;
                    default:
                        Debug.LogError("Unknown PowerUpID");
                        break;
                }
                
            }
            else
            {
                Debug.LogError("NullException - Error fetching player gameobject.");
            }
            
            Destroy(this.gameObject);
        }
    }
}
