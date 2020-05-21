using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdcamera : MonoBehaviour
{
    public GameObject object1;
    Vector3 distance;

    void Start()
    {
        distance = transform.position = object1.transform.position;
    }


    void LateUpdate()
    {
        transform.position = object1.transform.position + distance;
    }
}
