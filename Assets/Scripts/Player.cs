using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.5f;
    [SerializeField]
    private float _fireRate = 0.2f;
    private float _horizontalInput;
    private float _verticalInput;
    private float _canFire = -1f;
  
    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldVisual;

    private Vector3 laserPosition = new Vector3(0, 0.8f, 0);

    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _score;

    private SpawnManager _spawnManager;
    private UIManager _uiManager;


    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("SpawnManager is Null");
        }

        if(_uiManager == null)
        {
            Debug.LogError("UIManager is Null");
        }
    }

    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {

        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * _horizontalInput * speed * Time.deltaTime);
        transform.Translate(Vector3.up * _verticalInput * speed * Time.deltaTime);


        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }


        if (transform.position.x >= 11.5f)
        {
            transform.position = new Vector3(-11.5f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.5f)
        {
            transform.position = new Vector3(11.5f, transform.position.y, 0);
        }

    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position + laserPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + laserPosition, Quaternion.identity);
        }

    }

    public void Damage()
    {
        
        if(_isShieldActive == true)
        {
            return;
        }

        _lives -= 1;

        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(PowerDownRoutine());
    }

    public void SpeedPowerupActive()
    {
        _isSpeedBoostActive = true;
        StartCoroutine(PowerDownRoutine());
    }

    public void ShieldPowerupActive()
    {
        _isShieldActive = true;
        StartCoroutine(PowerDownRoutine());
    }

    IEnumerator PowerDownRoutine()
    {
        while (_isTripleShotActive == true)
        {
            yield return new WaitForSeconds(5.0f);
            _isTripleShotActive = false;
        }

        while(_isSpeedBoostActive == true)
        {
            speed = 8.5f;
            yield return new WaitForSeconds(5.0f);
            speed = 5.0f;
            _isSpeedBoostActive = false;
        }

        while(_isShieldActive == true)
        {
            _shieldVisual.SetActive(true);
            yield return new WaitForSeconds(5.0f);
            _shieldVisual.SetActive(false);
            _isShieldActive = false;
        }
    }

    public void AddToScore()
    {
        _score += 10;
        _uiManager.UpdateScore(_score);
        //communicate with UI to add score
    }

    
}


