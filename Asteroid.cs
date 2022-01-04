using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 18.0f;
    [SerializeField]
    private GameObject _ExplosionPrefab;
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //rotate object on z axis
        //speed = 18
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);


    }
    //check for laser collision (Trigger)
    //instantiate collision at the position of the asteroid
    //destroy the explosion after 3 seconds
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
            {

                Instantiate(_ExplosionPrefab, transform.position, Quaternion.identity);
                //Destroy Laser
                Destroy(other.gameObject);
                // Initiates/starts game objects to enter on screen
                _spawnManager.StartSpawning();
                Destroy(this.gameObject, 0.23f);
            }
    }

}

