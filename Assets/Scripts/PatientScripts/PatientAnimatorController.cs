using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientAnimatorController : MonoBehaviour
{
    [SerializeField] string[] initialAnimationStateList;
    [SerializeField] GameObject[] modelPrefabs;
    public string initial_animation_state;
    // Start is called before the first frame update
    void Start()
    {
        GameObject modelPrefab= Instantiate(modelPrefabs[Random.Range(0, modelPrefabs.Length)], gameObject.transform);
        initial_animation_state = initialAnimationStateList[Random.Range(0, initialAnimationStateList.Length)];
        GetComponentInChildren<Animator>().SetBool(initial_animation_state,true);

    }

    
}
