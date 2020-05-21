using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class Interactable : MonoBehaviour  //Bu class bizim butun interaction olan objectleriiz ucun base class olacaq. Bu classda oyuncunun interact etdiyi obyetklerin ortaq xususiyyetleri qeyd olunacaq.
{
    public float radius = 3f;             //interactable objecte nece metrden tesir ede bilerik onu temin edir.
    Dictionary<string, int> itemDict = new Dictionary<string, int>();

    //private void OnDrawGizmosSelected()   //Object secilende etrafinda Gizmo(Sablon goruntu) cekmesi ucun istifade edilir.
    //{
    //    Gizmos.color = Color.red;        //Bunun vasitesile gizmonun rengini teyin edirik.
    //    Gizmos.DrawWireSphere(transform.position, radius); //Bunun vasitesile gizmonu cekirik.
    //}
}
