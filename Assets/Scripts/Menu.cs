using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject resumeButton;
    public GameObject restartButton;
    public GameObject quitButton;
    public GameObject menuPanel;
    GameController gameController;
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Escape"))
        {
            if (gameController.paused)
            {
                menuPanel.SetActive(false);
                gameController.Unpause();
            }
            else
            {
                menuPanel.SetActive(true);
                gameController.Pause();
            }
        }
    }

    public void Resume()
    {
        menuPanel.SetActive(false);
        gameController.Unpause();
    }

    public void Restart()
    {
        gameController.ResetLevel();
    }

    public void Quit()
    {
        gameController.Quit();
    }
}
