using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public GameObject other;
    Vector3 origin;
    Vector3 target;
    int mode = 0;
    void Start()
    {
        origin = transform.position;
        target = other.transform.position;
    }

    void Update()
    {
       transform.position = Vector3.Lerp(transform.position, target, .02f); 
       if(Vector2.Distance(transform.position, target) <= 1){
           if(mode == 0){
               mode = 1;
               target = origin;
           }
           else{
               mode = 0;
               target = other.transform.position;
           }
       }
    }
}
