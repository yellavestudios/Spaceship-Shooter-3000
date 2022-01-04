using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;

    private void Update()
    {
        //if r key is pressed
        // restart the current scene
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(1);
        }

        //if esc key is pressed
        //quit game
        if (Input.GetKeyDown(KeyCode.Escape)) {

            Application.Quit();
        }
    }

       


    public void GameOver()
    {
        _isGameOver = true;
        
    }
}
