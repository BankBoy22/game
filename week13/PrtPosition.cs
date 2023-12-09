using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrtPosition : MonoBehaviour
{
    // Start is called before the first frame update
    float startingPoint;
    void Start()
    {
        Debug.Log("Start");
        startingPoint = transform.position.z;
    }
    // Update is called once per frame
    void Update()
    {
        float distance;
        distance = transform.position.z - startingPoint;
        Debug.Log("Distance: " + distance);
    }

}
