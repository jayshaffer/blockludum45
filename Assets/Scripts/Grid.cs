using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int gridSize;
    public GameObject gridBox;

    void Awake()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        for(int i = 0; i < gridSize; i++){
            for(int j = 0; j < gridSize; j++){
                Instantiate(gridBox, new Vector3(x, y, 0), Quaternion.identity);
                x++;
            }
            y--;
            x = transform.position.x;
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }

    void OnDrawGizmos() {
        Gizmos.DrawSphere(transform.position, 1);
    }
}
