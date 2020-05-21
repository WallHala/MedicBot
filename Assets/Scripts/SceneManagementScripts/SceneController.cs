using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public Transform[] IllSpawnPoint;
    [SerializeField] Transform[] MedicineSpawnPoint;
    [SerializeField] GameObject IllPrefab;
    [SerializeField] GameObject[] MedicineList;
    void Start()
    {
        for(int i = 0; i < IllSpawnPoint.Length; i++)
        {
            GameObject Ill=Instantiate(IllPrefab,IllSpawnPoint[i].transform);
            Ill.transform.position = IllSpawnPoint[i].transform.position;
        }





    }

}
