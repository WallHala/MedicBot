using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraswitch : MonoBehaviour
{
    public GameObject camera1;
    public GameObject camera3;
    public int cammode;


    void Update()
    {
        if (Input.GetButtonDown("CameraSwitch"))
        {
            if (cammode == 1)
            {
                cammode = 0;
            }
            else
            {
                cammode += 1;
            }
            StartCoroutine(camcange());
        }



    }

    IEnumerator camcange()

    {
        yield return new WaitForSeconds(0.01f);
        if (cammode == 0)
        {
            camera1.SetActive(false);
            camera3.SetActive(true);
        }
        if (cammode == 1)
        {
            camera1.SetActive(true);
            camera3.SetActive(false);
        }

    }


}
