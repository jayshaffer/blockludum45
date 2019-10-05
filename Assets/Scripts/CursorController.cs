using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public float speed;
    bool canAdjust = true;
    Rigidbody2D _rb;
    Vector3 direction;
    Vector3 origin;
    Vector3 lastMovePos;
    void Start()
    {
       _rb = GetComponent<Rigidbody2D>(); 
       origin = transform.position;
       lastMovePos = new Vector3(-100, -100, 0);
    }

    void Update()
    {
       if(Input.GetButton("Left") && canAdjust){
           TriggerMove(new Vector3(-1, 0, 0) * speed);
       }
       if(Input.GetButton("Up") && canAdjust){
           TriggerMove(new Vector3(0, 1, 0) * speed);
       }
       if(Input.GetButton("Down") && canAdjust){
           TriggerMove(new Vector3(0, -1, 0) * speed);
       }
       if(Input.GetButton("Right") && canAdjust){
           TriggerMove(new Vector3(1, 0, 0) * speed);
       }
    }

    void TriggerMove(Vector3 velocity){
        direction = velocity;
        canAdjust = false;
        lastMovePos = transform.position;
    }

    void FixedUpdate(){
        Debug.Log(transform.position.x - lastMovePos.x );
       if(Vector3.Distance(transform.position, lastMovePos) >= 1){
           Debug.Log("Adjust Allowed");
           canAdjust = true;
       }
       _rb.velocity = direction * Time.fixedDeltaTime;
    }
}
