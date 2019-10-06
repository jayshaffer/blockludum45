using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public float speed;
    public GameObject platform;
    public float time;
    public float dropRate;
    public bool spawnBricks = false;
    public bool phasing = false;
    public GameObject attentionGrabber;
    public bool autoStart;
    bool canAdjust = true;
    Rigidbody2D _rb;
    MeshRenderer meshRenderer;
    Vector3 direction;
    Vector3 origin;
    Vector3 lastMovePos;
    bool moving = false;
    Vector3 lastCubeDropPos;
    bool dead = false;
    GameController gameController;
    int lastDirection = 0;
    Dictionary<int, int> reverseDirectionMap = new Dictionary<int, int> { { 1, 3 }, { 2, 4 }, { 4, 2 }, { 3, 1 } };
    float nextFlash;
    float canMoveAt;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        origin = transform.position;
        lastMovePos = new Vector3(-100, -100, 0);
        lastCubeDropPos = new Vector3(-100, -100, 0);
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        nextFlash = CalcNextFlash(); 
        meshRenderer = GetComponent<MeshRenderer>();
        StartCoroutine("LoopFlash");
        canMoveAt = Time.time + 1;
    }

    float CalcNextFlash(){
        return Time.time + (time / 5);
    }

    void Update()
    {
        if (dead || Time.time < canMoveAt)
        {
            return;
        }
        if(autoStart){
            TriggerMove(new Vector3(0, -1, 0) * speed, 1);
        }
        if (Input.GetButton("Left") && canAdjust)
        {
            TriggerMove(new Vector3(-1, 0, 0) * speed, 1);
        }
        if (Input.GetButton("Up") && canAdjust)
        {
            TriggerMove(new Vector3(0, 1, 0) * speed, 2);
        }
        if (Input.GetButton("Down") && canAdjust)
        {
            TriggerMove(new Vector3(0, -1, 0) * speed, 4);
        }
        if (Input.GetButton("Right") && canAdjust)
        {
            TriggerMove(new Vector3(1, 0, 0) * speed, 3);
        }
        if(moving){
            time -= Time.deltaTime;
        }
        if(time <= 0){
            Debug.Log("here");
            Kill();
        }
    }

    public void Kill()
    {
        gameController.ControlPlayer();
        GameObject.Destroy(gameObject, 0);
        dead = true;
        _rb.velocity = new Vector3(0, 0, 0);
    }

    public void PhaseOn(){
        if(!moving){
            return;
        }
        spawnBricks = false;
        phasing = true;
        GetComponent<Renderer>().material.color = Color.gray;
    }
    
    public void PhaseOff(){
        spawnBricks = true;
        phasing = false;
        GetComponent<Renderer>().material.color = Color.red;
    }

    void TriggerMove(Vector3 velocity, int directionKey)
    {
        if(!moving){
            spawnBricks = true;
            attentionGrabber.SetActive(false);
        }
        if (reverseDirectionMap[directionKey] == lastDirection)
        {
            return;
        }
        DropBlock();
        direction = velocity;
        canAdjust = false;
        lastMovePos = transform.position;
        moving = true;
        lastDirection = directionKey;
    }

    void DropBlock()
    {
        if(!spawnBricks){
            return;
        }
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, -1);
        GameObject clone = Instantiate(platform, newPos, Quaternion.identity);
        clone.transform.localScale = clone.transform.localScale * .8f;
        clone.GetComponent<Renderer>().material.color = Random.ColorHSV();
        lastCubeDropPos = clone.transform.position;
    }

    void FixedUpdate()
    {
        if (dead)
        {
            return;
        }
        if (Mathf.Abs(transform.position.x - lastMovePos.x) >= .6 || Mathf.Abs(transform.position.y - lastMovePos.y) >= .6)
        {
            canAdjust = true;
        }
        if (spawnBricks && Vector3.Distance(lastCubeDropPos, transform.position) >= dropRate)
        {
            DropBlock();
        }
        _rb.velocity = direction * Time.fixedDeltaTime;
    }

    IEnumerator LoopFlash(){
        while(true){
            if(Time.time > nextFlash){
                meshRenderer.enabled = false;
                nextFlash = CalcNextFlash();
            }
            else{
                meshRenderer.enabled = true;
            }
            yield return new WaitForSeconds(.1f);
        }
    }
}
