using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 4f;
    [SerializeField] private GameObject _thruster;

    private Player _player;
    private Animator _anim;
    private AudioSource _audioSource;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        if(_player == null)
        {
            Debug.LogError("The Player is NULL!");
        }

        if(_anim == null)
        {
            Debug.LogError("The animator is NULL!");
        }

        if(_audioSource == null)
        {
            Debug.LogError("AudioSource on the enemy is NULL!");
        }
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
            if(_player != null)
            {
                _player.Damage();
            }

            OnDeath();
        }

        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            
            if(_player != null)
            {
                _player.AddScore(10);
            }
            
            OnDeath();
        }
    }

    public void OnDeath()
    {
        _anim.SetTrigger("OnEnemyDeath");
        _thruster.SetActive(false);
        Destroy(GetComponent<Collider2D>());
        Destroy(this.gameObject, 2.5f);
        _audioSource.Play();
    }
}
