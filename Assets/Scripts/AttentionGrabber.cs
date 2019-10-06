using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttentionGrabber : MonoBehaviour
{
    public float maxSize = 10;
    public float minSize = .5f;
    float interval = .5f;
    float modifier = 1;
    Vector3 maxVector;
    Vector3 minVector;
    Vector3 targetVector;

    void Start()
    {
        maxVector = gameObject.transform.localScale * 1.5f;
        maxVector.z = 0;
        minVector = gameObject.transform.localScale;
        targetVector = maxVector;
    }

    void Update()
    {
        Vector3 scale = gameObject.transform.localScale;
        Vector3 newScale = Vector3.Lerp(gameObject.transform.localScale, targetVector, .1f);
        newScale.z = scale.z;
        gameObject.transform.localScale = newScale;
        if( Vector2.Distance(newScale, maxVector) < .1f){
            targetVector = minVector;
        }
        if(Vector2.Distance(newScale, minVector) < .1){
            targetVector = maxVector;
        }
    }
}
