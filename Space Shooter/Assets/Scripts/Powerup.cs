using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField] private float _speed = 5f;
    [SerializeField] private int powerupID; //0 = Triple Shot, 1 = Speed, 2 = Shield

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = _speed * Vector3.down;

        if(transform.position.y <= -5.8f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            
            if(player != null)
            {
                switch(powerupID)
                {
                    case 0:
                        player.ActivateTripleShot();
                        break;
                    case 1:
                        player.ActivateSpeedBoost();
                        break;
                    case 2:
                        player.ActivateShield();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }

            Destroy(this.gameObject);
        }
    }
}
