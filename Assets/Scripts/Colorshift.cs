using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorshift : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine("ColorShift"); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ColorShift(){
        while(true){
            GetComponent<Renderer>().material.color = Random.ColorHSV();
            yield return new WaitForSeconds(1f);
        }
    }
}
