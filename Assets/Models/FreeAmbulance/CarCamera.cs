using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class CarCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = 8f;
    public float yMaxLimit = 30f;

    public float distanceMin = 5f;
    public float distanceMax = 15f;

    private Rigidbody rigidbody;

    float x = 0.0f;
    float y = 0.0f;

    private int CameraView = 0;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.V) && CameraView == 0)
        {
            distanceMin = 15f;
            distanceMax = 15f;
            yMinLimit = 12f;
            yMaxLimit = 40f;
            CameraView = 2;
        }
        else if (Input.GetKey(KeyCode.V) && CameraView == 1)
        {
            distanceMin = 12f;
            distanceMax = 12f;
            yMinLimit = 8f;
            yMaxLimit = 36f;
            CameraView = 0;
        }
        else if (Input.GetKey(KeyCode.V) && CameraView == 2)
        {
            distanceMin = 10f;
            distanceMax = 10f;
            yMinLimit = 5f;
            yMaxLimit = 32f;
            CameraView = 1;
        }
    }
    void LateUpdate()
    {
        if (target)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            RaycastHit hit;
            if (Physics.Linecast(target.position, transform.position, out hit))
            {
                distance -= hit.distance;
            }
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
