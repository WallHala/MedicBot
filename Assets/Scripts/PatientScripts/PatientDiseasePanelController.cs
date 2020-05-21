using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PatientDiseasePanelController : MonoBehaviour
{
    #region Serialized Variables

    [SerializeField] List<string> Symptom_list;                                             //Simptomlarin adlarini dasiyan listi temsil edir.Butun simptomlar bu listde olacaq.
    [SerializeField] List<string> CureList;                                                 //Hansi xesteliyi ne mualice edecek xesteliklerin siyahisina uygun yerlesdirilmelidir.
    [SerializeField] List<string> owned_symptoms;                                           //Hazirki oyuncunun dasidigi simptomlari temsil edir.
    [SerializeField] List<string> owned_cure;                                               //Hazirki oyuncunun dasidigi xesteliklerin mualicesinin adlarini ozunde sira ile saxlayir.
    [SerializeField] GameObject Ilness_recovery_status_panel;                               //Bu panelde xesteliklerin adlari ve qarsilarinda qus(tick) qoymaq ucun yerler olacaq.
    [SerializeField] GameObject Symptom_text_prefab;                                        //Bu prefabi ve tiki instance edib yan yana qoyaciyiq.
    [SerializeField] GameObject YouWinText;                                                 //Bu text butun xesteleri mualice etdikde ise dusecek.
    #endregion

    #region Private Variables

    Canvas Symptoms_text_Canvas;                                                            //Simptom textlerini dasiyan canvasi temsil edir.
    int ilness_cured_count;                                                                 //Bu sagalan xesteleriklerin siyahisini gosterir.Animasiyalar arasinda kecid etmek ucun bundan istifade edeceyik.
    #endregion




    #region MonoBehaviour Callbacks
    void Start()
    {
        Symptoms_text_Canvas = GetComponentInChildren<Canvas>();                            //Сanvas child obyektde oldugu ucun onu aliriq.
        Symptoms_text_Canvas.enabled = false;                                               //Ilk basda canvasi sondururuk ki, onTriggerEnterde aktivlesdire bilek.
        Symptoms_text_Canvas.worldCamera = FindObjectOfType<playermove>().currentCam;       //Canvasin render camerasina hazirki aktiv camerani yerlesdiririk.
        TextMeshProUGUI symptom_text = GetComponentInChildren<TextMeshProUGUI>();           //simptomlarin adlarinin duzuleceyi TMPro objectini aliriq.
        byte symptom_count = (byte)UnityEngine.Random.Range(1, 5);                                      //Patient nece dene simptom dasiyacaq onu temin edirik.
        YouWinText = GameObject.Find("YouWinText");
        Debug.Log(YouWinText.tag);
        for (int i = 0; i < symptom_count; i++)
        {
            byte random_symptom_index = (byte)UnityEngine.Random.Range(0, Symptom_list.Count);          //Simptomlar random sekilde olsun deye bele teyin edirik.

            if (owned_symptoms.Contains(Symptom_list[random_symptom_index].ToLower()))                //Eger symptom siyahimizda bu adda simptom varsa (Random eyni reqemi tekrarliya biler)
            {
                owned_symptoms.Remove(Symptom_list[random_symptom_index].ToLower());                  //Onu kenarlasdiririq.
                GameObject copy = GameObject.Find(Symptom_list[random_symptom_index].ToLower());
                DestroyImmediate(copy.gameObject);                                                             //Bu adda bir text objecti yaradib.Kopya olmasin deye Hemin objecti mehv edirik.
                owned_cure.Remove(CureList[random_symptom_index].ToLower());                          //Ve owned_cure_listden de owned_symptoma aid hisseni silir.
            }


            owned_symptoms.Add(Symptom_list[random_symptom_index].ToLower());                         //Ve hazirki simptomu elave edirik.
            owned_cure.Add(CureList[random_symptom_index].ToLower());                               //hazirki simptoma aid careni care(cure) siyahisina elave edirik.

            GameObject text_field = Instantiate(Symptom_text_prefab, Ilness_recovery_status_panel.transform);

            text_field.name = Symptom_list[random_symptom_index].ToLower();

            text_field.GetComponent<TextMeshProUGUI>().text = Symptom_list[random_symptom_index].ToLower();
        }
    }       
        

        //symptom_text.text = " ";                                                          //simptom textini 0-layiriq.
        //for (int i = 0; i < owned_symptoms.Count; i++)
        //{
        //    GameObject text_field= Instantiate(Symptom_text_prefab, Ilness_recovery_status_panel.transform);
        //    text_field.GetComponent<TextMeshProUGUI>().text = owned_symptoms[i];
        //    //symptom_text.text = symptom_text.text + " \n" + owned_symptoms[i];
        //}
    
    #endregion



    #region Physics Callbacks
    private void OnTriggerEnter(Collider other)
    {
        Symptoms_text_Canvas.referencePixelsPerUnit = -10;                                  //Canvasin referans pikselini deyisirik.
        for(int i = 0; i < owned_symptoms.Count; i++)
        {
            GameObject InventoryPanelObject = GameObject.Find(owned_cure[i].ToLower());                         //Robot obyektinde her bir derman-spris ve ya diger sagaldici seylerin adini gameObjectin adina elave etmisik. Bunun vasitesile hemin boyekti tapiriq.
            if (byte.Parse(InventoryPanelObject.transform.parent.GetComponentInChildren<Text>().text)>0)        //Hemin object Symptomps_textPefabdir. O objectin childinda itemlarin asynin gosteren text objectinin textinin 0-dan boyuk olub-olmamasini yoxlayiriq.
            {
                GameObject SymptomToggleParent= GameObject.Find(owned_symptoms[i].ToLower());
                Debug.Log(SymptomToggleParent.name);
                if(SymptomToggleParent.GetComponentInChildren<Toggle>().isOn == false)
                {
                    SymptomToggleParent.GetComponentInChildren<Toggle>().isOn = true;
                    ilness_cured_count += 1;
                    InventoryPanelObject.transform.parent.GetComponentInChildren<Text>().text = (int.Parse(InventoryPanelObject.transform.parent.GetComponentInChildren<Text>().text)-1).ToString();
                }
            }
            
        }
        if (ilness_cured_count == owned_symptoms.Count)
        {
            GetComponentInChildren<Animator>().SetBool(GetComponent<PatientAnimatorController>().initial_animation_state, false);
            GetComponentInChildren<Animator>().SetTrigger("Cured");
            FindObjectOfType<playermove>().cured_ill_people_number += 1;
            if (FindObjectOfType<playermove>().cured_ill_people_number == FindObjectOfType<SceneController>().IllSpawnPoint.Length)
            {
                FindObjectOfType<playermove>().enabled = false;
                FindObjectOfType<mouselook>().enabled = false;
                YouWinText.GetComponent<Canvas>().enabled = true;
            }
        }
        Symptoms_text_Canvas.enabled = true;
    }


    private void OnTriggerExit(Collider other)
    {
        Symptoms_text_Canvas.enabled = false;
    }
    #endregion
}
