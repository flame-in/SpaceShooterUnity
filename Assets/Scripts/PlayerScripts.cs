using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScripts : MonoBehaviour
{
    [SerializeField]
    private float _speed = 6.5f;
    [SerializeField]
    private float _speedBoost = 9f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.2f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private bool _isTripleShotEnabled = false;
    [SerializeField]
    private bool _isSpeedEnabled = false;
    [SerializeField]
    private bool _isShieldEnabled = false;
    private SpawnManagerScript _spawnManager;
    private UIManager _UIManager;
    [SerializeField]
    private GameObject _laserContainer;
    [SerializeField]
    private GameObject _shieldsAura;

    [SerializeField]
    private int _score;


    // Start is called before the first frame update
    void Start()
    {
        _shieldsAura.SetActive(false);
        transform.position = new Vector3(0, -2f, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManagerScript>();
        if (_spawnManager == null)
        {
            Debug.LogError("The spawnManager is null.");
        }
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_UIManager == null)
        {
            Debug.LogError("The UIManager is null.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            fireLaser();
        }
    }

    void calculateMovement ()
    {
        // Input movement = Position (x,y,0) * Speed (horizontalSpeed, verticalSpeed, 0) - vector multiplication
        // Vector3 playerInput = Vector3.Scale(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0), new Vector3(_horizontalSpeed, _verticalSpeed, 0));
        Vector3 playerInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        if (_isSpeedEnabled)
        {
            transform.Translate(playerInput * _speedBoost * Time.deltaTime);
        }
        transform.Translate(playerInput * _speed * Time.deltaTime);

        // Clamping player to vertical playable area ( Y -> -5 to 0 )
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y,-5f,0f), 0);

        // Wrapping player around playable area ( X -> -11 to 11 )
        if (transform.position.x < -11f)
        {
            transform.position = new Vector3(11f, transform.position.y, 0);
        }
        else if (transform.position.x > 11f)
        {
            transform.position = new Vector3(-11f, transform.position.y, 0);
        }
    }

    // Player firing weapon
    void fireLaser()
    {
        _canFire = Time.time + _fireRate;
        if (_isTripleShotEnabled)
        {
            GameObject newTripleShot = Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            newTripleShot.transform.parent = _laserContainer.transform;
        }
        else
        {
            GameObject newLaserShot = Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            newLaserShot.transform.parent = _laserContainer.transform;
        }
        

    }

    // Player taking damage - public method
    public void DamagePlayer()
    {
        if (!_isShieldEnabled)
        {
            _lives -= 1;
            _UIManager.UpdateLives(_lives);
            Debug.Log("Player is hit. Current life is : " + _lives);
            if (_lives < 1)
            {
                _spawnManager.OnPlayerDeath();
                Destroy(this.gameObject);
            }
        }
        else
        {
            _shieldsAura.SetActive(false);
            _isShieldEnabled = false;
            Debug.Log("Shield powerup has been DESTROYED.");
        }
        
        
    }

    //Public method to enable triple shot powerUp - also starts coRoutine to disable the powerup in 5 seconds
    public void EnableTripleShot()
    {
        _isTripleShotEnabled = true;
        StartCoroutine(TripleShotPowerDownRoutine());

    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotEnabled = false;
        Debug.Log("Triple shot powerup has been disabled.");
    }

    public void EnableSpeedBoost()
    {
        _isSpeedEnabled = true;
        StartCoroutine(SpeedPowerDownRoutine());
    }
    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedEnabled = false;
        Debug.Log("Speed powerup has been disabled.");
    }
    public void EnableShields()
    {
        _isShieldEnabled = true;
        _shieldsAura.SetActive(true);
        StartCoroutine(ShieldsPowerDownRoutine());
    }
    IEnumerator ShieldsPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isShieldEnabled = false;
        _shieldsAura.SetActive(false);
        Debug.Log("Shield powerup has been disabled.");
    }

    public void addScore(int points)
    {
        _score += points;
        _UIManager.UpdateScore(_score);
    }
}
