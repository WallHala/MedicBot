using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnter : MonoBehaviour
{
    public GameObject camera_Car;
    public GameObject car;
    public CarControllerScript CCS;
    public GameObject Player;
    public GameObject EmptyParent;
    //public GameObject camera_Player;
    private bool isEnter;
    private bool isPlayer;

    void Update()
    {
        if (isPlayer == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (isEnter == false)
                {
                    camera_Car.SetActive(true);
                    Player.SetActive(false);
                    //camera_Player.SetActive(false);
                    CCS.enabled = true;
                    Player.transform.parent = car.transform;
                    isEnter = true;
                }
                else
                {
                    camera_Car.SetActive(false);
                    Player.SetActive(true);
                    //camera_Player.SetActive(true);
                    CCS.enabled = false;
                    Player.transform.parent = EmptyParent.transform;
                    isEnter = false;
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (isEnter == true)
                {
                    camera_Car.SetActive(false);
                    Player.SetActive(true);
                    //camera_Player.SetActive(true);
                    CCS.enabled = false;
                    Player.transform.parent = EmptyParent.transform;
                    isEnter = false;
                }
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            isPlayer = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            isPlayer = false;
        }
    }
}
