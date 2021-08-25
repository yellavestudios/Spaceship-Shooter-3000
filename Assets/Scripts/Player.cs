using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{
    [SerializeField]
    private float _speed = 3.2f;
    
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    //determine if we can fire
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        //take the current position = new position (0, 0, 0);
        transform.position = new Vector3(0, 0, 0);
        //find the object.. get the component
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();


        if (_spawnManager == null)
        {

            Debug.LogError ("The Spawn Manager is Null!");
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

        //new Vector3((1, 0, 0) * 3.2f * 0 *  * real time);
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

            //run this code
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
       Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        

    }

    public void Damage()
    {

        _lives -= 1;


        if (_lives < 1)
        {
            //Communicate with the SpawnManager
            //stop spawning
            _spawnManager.OnPlayerDeath();            
            Destroy(this.gameObject);
        }    
    }

}
