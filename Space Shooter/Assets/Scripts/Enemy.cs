using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 4f;

    void Start()
    {
        
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = _moveSpeed * Vector3.down;

        if(transform.position.y <= -5.5f)
        {
            float randomX = Random.Range(-8.4f, 8.4f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }

            Destroy(this.gameObject);
        }

        if(other.tag == "Laser")
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
}
