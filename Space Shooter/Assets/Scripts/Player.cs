using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{ 
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private float _timeBetweenShots;
    [SerializeField] private int _lives = 3;

    private Vector2 _movementInput;
    private bool _canFire;
    private float _lastFireTime;
    private SpawnManager _spawnManager;
    [SerializeField] private bool _isTripleShotActive = false;

    void Start()
    {
        transform.position = new Vector3(0, -3.8f, 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL!");
        }
    }

    void Update()
    {
        Move();

        FireLaser();
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
        if(_canFire)
        {
            float timeSinceLastFire = Time.time - _lastFireTime;

            if(timeSinceLastFire >= _timeBetweenShots)
            {
                if(_isTripleShotActive)
                {
                    Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(_laserPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity); 
                }
                
                _lastFireTime = Time.time;
                _canFire = false;
            }
        }

    }

    public void Damage()
    {
        _lives--;

        if(_lives == 0)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void ActivateTripleShot()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    private void OnMove(InputValue value)
    {
        _movementInput = value.Get<Vector2>();
        //Debug.Log(movementInput);
    }

    private void OnFire(InputValue value)
    {
        _canFire = value.isPressed;
    }
    
}
