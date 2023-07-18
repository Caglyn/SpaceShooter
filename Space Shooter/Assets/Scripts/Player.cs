using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector2 _movementInput;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private float _laserSpeed;
    [SerializeField] private float _timeBetweenShots;


    private bool _fireSingle;
    private float _lastFireTime;

    void Start()
    {
        transform.position = new Vector3(0, -4.2f, 0);
    }

    void Update()
    {
        Move();

        if(_fireSingle)
        {
            float timeSinceLastFire = Time.time - _lastFireTime;

            if(timeSinceLastFire >= _timeBetweenShots)
            {
                FireLaser();

                _lastFireTime = Time.time;
                _fireSingle = false;
            }
            
        }
    }

    private void Move()
    {
        Vector3 delta = _movementInput * _moveSpeed * Time.deltaTime;
        transform.position += delta;

        //bound for y-axis
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.2f, -2f), 0);
        
        if(transform.position.x >= 9.3f)
        {
            transform.position = new Vector3(-8.5f, transform.position.y, 0);
        }
        else if(transform.position.x <= -9.3f)
        {
            transform.position = new Vector3(8.5f, transform.position.y, 0);
        }
    }

    private void FireLaser()
    {
        GameObject laser = Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        Rigidbody2D rigidbody = laser.GetComponent<Rigidbody2D>();

        rigidbody.velocity = _laserSpeed * transform.up;
    }

    private void OnMove(InputValue value)
    {
        _movementInput = value.Get<Vector2>();
        //Debug.Log(movementInput);
    }

    private void OnFire(InputValue value)
    {
        _fireSingle = value.isPressed;
    }
    
}
