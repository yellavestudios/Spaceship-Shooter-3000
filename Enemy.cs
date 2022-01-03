using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        //move down at 4 meters per second
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        
        //if at bottom of screen
        //respawn at top of screen with new random x position

        if (transform.position.y < -5.3f)
        {

            float randomX = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(randomX, 7.4f, 0);

        }


    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        //if other is player
        //damage the player
        //Destroy us

        if (other.tag == "Player")
        {
            //damage player
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {

                player.Damage();
            }

            Destroy(this.gameObject);

        }

        //if other is laser
        //laser
        //Destory us

        if (other.tag == "Laser")
        {
            //add 10 to score
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddScore(10);
            }

            Destroy(this.gameObject);

        }
    }
}
