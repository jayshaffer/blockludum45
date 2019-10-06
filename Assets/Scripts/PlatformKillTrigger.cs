using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformKillTrigger : MonoBehaviour
{
    public bool canPhase = false;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "PlatformSpawner"){
            CursorController cursorController = other.gameObject.GetComponent<CursorController>(); 
            if(!cursorController.phasing || !canPhase){
                cursorController.Kill();
            }
        }
    }
    private void OnDrawGizmos() {
        Gizmos.DrawCube(transform.position, new Vector3(1,1,0));
    }
}
