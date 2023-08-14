using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField]    private float _bulletSpeed = 4f;
   
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _bulletSpeed * Time.deltaTime);

        if (transform.position.y > 7f)
        {
            Destroy(this.gameObject);
        }
    }
}
