using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrigger : MonoBehaviour
{
    public bool onGround;
    List<GameObject> colliding;
    
    void Start()
    {
        colliding = new List<GameObject>();
    }

    void Update()
    {
        onGround = colliding.Count != 0;
        Debug.Log(colliding.Count);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            colliding.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        colliding.Remove(other.gameObject);
    }
}
