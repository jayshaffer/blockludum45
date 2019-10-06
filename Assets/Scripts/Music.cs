using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    AudioSource music;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void Awake(){
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
}
