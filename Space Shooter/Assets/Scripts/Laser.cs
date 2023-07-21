using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _laserSpeed = 5f;

    void Update()
    {
        Move();
        
    }

    private void Move()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = _laserSpeed * transform.up;

        if(transform.position.y >= 6f)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            
            Destroy(this.gameObject);
        }
    }
}
