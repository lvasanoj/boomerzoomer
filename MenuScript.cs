using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    //Script for all ui, text, menus etc.
    public Text gameOverText;

    public Text pressKey;
    public bool startGame = false;
    public Object sceneToLoad;


    // Start is called before the first frame update
    void Start()
    {
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Level01");
    }

    public void StartGame()
    {
        startGame = true;
        pressKey.gameObject.SetActive(false);
    }

    public void PauseGame()
    {
        startGame = false;
        // Debug.Log("PAUSE THE FUCKIN GAME");
        pressKey.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit game");
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("Level01");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("r"))
        {
            RestartGame();
        }

        if(!startGame && Input.anyKey)
        {
            Debug.Log("Start Game");
            StartGame();
        }
        
    }
}
