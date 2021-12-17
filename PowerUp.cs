using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    private float _speed = 3.0f;
    //ID for Powerups
    //0 = Triple Shot
    //1 = Speed
    //2 = Shields
    [SerializeField]
    private int powerupID;
    
  
    // Update is called once per frame
    void Update()
    {
        //move down at a speed of 3 (adjust in the inspector)
        //when we leave the screen, destroy this object
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }
    }




    
    private void OnTriggerEnter2D(Collider2D other)
    {
        //collider with the player
        if (other.tag == "Player")
        {
            
            //grab the component
            Player player = other.transform.GetComponent<Player>();
            //active powerup and null check
            if (player != null)
            {

                switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        Debug.Log("Collected Shield");
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
                
            }

            //destroy powerup
            Destroy(this.gameObject);
        }
    }
}
