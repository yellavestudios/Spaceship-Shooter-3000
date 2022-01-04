using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{
    [SerializeField]
    private float _speed = 3.2f;
    private float _speedMultiplier = 2;
    
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    //determine if we can fire
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnManager;
    
    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldsActive = false;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private GameObject _rightEngine, _leftEngine;

    
    [SerializeField]
    private int _score;

    private UIManager _uiManager;
  

    // Start is called before the first frame update
    void Start()
    {
        //take the current position = new position (0, 0, 0);
        transform.position = new Vector3(0, 0, 0);
        //find the object.. get the component
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();


        if (_spawnManager == null)
        {

            Debug.LogError ("The Spawn Manager is Null!");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL!");
        }
    }

    // Update is called once per frame
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
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        
        transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime); 
        
          
            

        //if  player position on the y is greater than 0
        //y position = 0
        //else if position on the y is less than -3.4f
        //y pos = -3.4f

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.4f)
        {
            transform.position = new Vector3(transform.position.x, -3.4f, 0);
        }

        //wrapping player through both sides of game
        //if player on the x > 11
        //x pos = -11
        //else if player on the x is less than -11
        // x pos = 11


        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }

    }

    void FireLaser()
    {

         //if I hit the space key
        //spawn game object

       _canFire = Time.time + _fireRate;

        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);

        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
        //if space key press
        //if tripleshotActive is true
            //fire 3 lasers (triple shot prefab)

        //else fire 1 laser

        //instantiate 3 lasers (triple shot prefab)

    }

    public void Damage()
    {

        //if shields is activated
        //do nothing..
        //deactivate shields
        //return;

        if (_isShieldsActive == true)
        {
            _isShieldsActive = false;
            _shieldVisualizer.SetActive(false);
            return;

        }

        _lives -= 1;

        //if lives is 2
        //enable right engine
        //else if lives is 1
        //enable left engine
        if (_lives == 2)
        {
            _leftEngine.SetActive(true);
        }
        else if (_lives == 1)
        {
            _rightEngine.SetActive(true);
        }

        _uiManager.UpdateLives(_lives);


        if (_lives < 1)
        {
            //Communicate with the SpawnManager
            //stop spawning
            _spawnManager.OnPlayerDeath();            
            Destroy(this.gameObject);
        }    
    }

    

    public void TripleShotActive()
    {
        //tripleShotActive becomes true
        //start the power down coroutine for triple shot
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRountine());

    }

    IEnumerator TripleShotPowerDownRountine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        _isSpeedBoostActive = false;
        yield return new WaitForSeconds(5.0f);
        _speed /= _speedMultiplier;
    }

    public void ShieldsActive()
    {
        _isShieldsActive = true;
        _shieldVisualizer.SetActive(true);
    }

    //add method to add 10 to score
    //communicate with UI manager to update!
    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
