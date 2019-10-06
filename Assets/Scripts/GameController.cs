using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public CameraController cameraController;
    public GameObject cursorControllerObject;
    public bool controllingPlayer = false;
    public bool titleScreen;
    CursorController cursorController;
    PlayerController playerController;
    void Start()
    {
        cursorController = cursorControllerObject.GetComponent<CursorController>(); 
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void ControlPlayer(){
        if(playerController != null){
            playerController.unfreezeAt = Time.time + 1f;
            controllingPlayer = true;
        }
    }

    public void ResetLevel(){
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void NextLevel(){
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Update()
    {
        if(Input.GetButton("Switch")){
            ResetLevel();
        }
        if(Input.GetButtonDown("Jump") && titleScreen){
            NextLevel();
        }
        if(Input.GetButtonDown("Jump") && !controllingPlayer){
            if(cursorController.phasing){
                cursorController.PhaseOff();
            }
            else{
                cursorController.PhaseOn();
            }
        }
    }
}
