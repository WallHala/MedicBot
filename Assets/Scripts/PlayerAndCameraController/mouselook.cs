using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouselook : MonoBehaviour
{
    public Transform playerbody;

    public float sensivitym = 100f;
    float rot = 0.0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }



    void FixedUpdate()
    {

        float mouseY = Input.GetAxis("Mouse Y") * sensivitym * Time.deltaTime;
        float mouseX = Input.GetAxis("Mouse X") * sensivitym * Time.deltaTime;

        rot -= mouseY;
        rot = Mathf.Clamp(rot, -90f, 90f);
        transform.localRotation= Quaternion.Euler(rot, 0f, 0f);
        playerbody.Rotate(Vector3.up * mouseX);



    }
}
