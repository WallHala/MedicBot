using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CraftingSystemScript : MonoBehaviour
{
    [SerializeField] GameObject slot1;                  //Bu bizim Cavnvasdaki 1ci slotdur.
    [SerializeField] GameObject slot2;                  //Bu ise 2ci slotdur.
    [SerializeField] GameObject slotResult;             //Craftingin neticesini gostermek ucun bu slot istifade edilir.
    [SerializeField] string[] itemsListOnePart;         //Bu bizim crafting ucun istifade olunan elementlerin 1ci hissesidir.Burda stringler vasitesile elementlerin adini yaziriq.
    [SerializeField] string[] itemListSecondPart;       //Bu ise 2ci hissesidir.(Stringler).!DIQQET: canvasdaki elementlerin adlari ile(elementin altinda yazilan text) bu textler eyni olmalidir cunki kod adlara esasen isleyir.
    [SerializeField] string[] Results;                  //Neticlerin siyahisi olan string arraydir.

    private void Update()
    {
        CheckSlots(itemListSecondPart, itemsListOnePart);                                     //Burda iki defe yoxlayiriq cunki ola biler oyuncu slot1 ve slot2deki yerlerini ters qoysun.
        CheckSlots(itemsListOnePart, itemListSecondPart);
    }


    public void ClearSlots()                                                                 //Slotdaki elementleri silmek ucun istifade edilir.
    {
        if (slot1.transform.childCount != 0)                                                 //Eger slot1de child varsa
        {
            Destroy(slot1.transform.GetChild(0).gameObject);                                  //O child bizim verdiyimiz obyektdir ve onu mehv edirik.
        }
        if (slot2.transform.childCount != 0)                                                 //Eger slot2de child varsa
        {
            Destroy(slot2.transform.GetChild(0).gameObject);
        }

        StartCoroutine(AddResultToInventory());
        slot1.GetComponent<CraftPanelSlotScripts>().IsEmpty = true;                         //slot1 ve 2deki CraftPanelSlotScriptdeki isEmpty-ni true edirik ki, asagidaki metodda istifade ede bilek.
        slot2.GetComponent<CraftPanelSlotScripts>().IsEmpty = true;
        slotResult.GetComponent<CraftPanelSlotScripts>().IsEmpty = true;
        Destroy(GameObject.Find("New Game Object"));
    }


    public void CheckSlots(string[] array1,string[] array2)
    {
        if (slot1.GetComponent<CraftPanelSlotScripts>().IsEmpty == false && slot2.GetComponent<CraftPanelSlotScripts>().IsEmpty == false)//Slot1 ve slot2-nin 2ninde dolu olub- olmadigini yoxlayiriq.
        {
            for (int i = 0; i < array1.Length; i++)                                         //1ci arraydeki elementlere gore muqayise edirik.
            {
                if (slot1.transform.GetChild(0).name.ToLower() == array1[i].ToLower())      //Eger slot1in childinin adi arraydeki i elementinin childinin adi ile eynidise
                {
                    
                    if (slot2.transform.GetChild(0).name.ToLower() == array2[i].ToLower()&&slotResult.GetComponent<CraftPanelSlotScripts>().IsEmpty==true) //Ve eger hemin indexdeki array2nin elementinin adi da slot2nin childinin adi ile eynidise
                    {
                       GameObject result= Instantiate(new GameObject(), slotResult.transform);    //Neticeni goster.
                        
                        result.AddComponent<Image>().sprite=GameObject.Find(Results[i].ToLower()).GetComponent<Image>().sprite;
                        result.GetComponent<Image>().preserveAspect = true;
                        result.name = Results[i].ToLower();
                        slotResult.GetComponent<CraftPanelSlotScripts>().IsEmpty =false;
                    }
                }
            }
        }
    }

    IEnumerator AddResultToInventory()
    {
        if (slotResult.transform.childCount != 0)                                           //Netice slotunda child varsa
        {
            GameObject inventoryGameObject = GameObject.Find(slotResult.transform.GetChild(0).name.ToLower());
            Debug.Log(inventoryGameObject.name);
            inventoryGameObject.transform.parent.GetComponentInChildren<Text>().text = (byte.Parse(inventoryGameObject.transform.parent.GetComponentInChildren<Text>().text) + 1).ToString();
            yield return new WaitForSeconds(0.5f);
            Destroy(slotResult.transform.GetChild(0).gameObject);
            StopCoroutine(AddResultToInventory());
        }
    }
}
