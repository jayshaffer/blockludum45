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
    public bool paused = false;
    CursorController cursorController;
    PlayerController playerController;
    
    void Awake(){
    }

    void Start()
    {
        if(cursorControllerObject != null){
            cursorController = cursorControllerObject.GetComponent<CursorController>(); 
        }
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void ControlPlayer(){
        if(playerController != null){
            playerController.unfreezeAt = Time.time + 1f;
            controllingPlayer = true;
        }
    }

    public void ResetLevel(){
        Unpause();
        Scene scene = SceneManager.GetActiveScene(); 
        if(scene.buildIndex == 0){
            GameObject.Destroy(GameObject.FindGameObjectWithTag("Music"));
        }
        SceneManager.LoadScene(scene.name);
    }

    public void NextLevel(){
        Scene scene = SceneManager.GetActiveScene(); 
        int index = scene.buildIndex + 1;
        Debug.Log(SceneManager.sceneCount);
        if(SceneManager.sceneCountInBuildSettings > index){
            SceneManager.LoadScene(index);
        }
        else{
            GameObject.Destroy(GameObject.FindGameObjectWithTag("Music"));
            SceneManager.LoadScene(0);
        }
    }

    public void Pause(){
        paused = true;
        Time.timeScale = 0.0f;
    }

    public void Unpause(){
        paused = false;
        Time.timeScale = 1.0f;
    }

    public void Quit(){
        Application.Quit();
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
