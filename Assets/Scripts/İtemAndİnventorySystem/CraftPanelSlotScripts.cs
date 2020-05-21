using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// Bu script ise drag edilmis objectin yerleseceyi slotu idare etmek ucun istifade edilir.
/// </summary>
public class CraftPanelSlotScripts : MonoBehaviour , IDropHandler
{
    public bool IsEmpty;


    private void Start()
    {
        IsEmpty = true;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if ((IsEmpty==true)&&gameObject.name!="Result")
        {
            if (eventData.pointerDrag.gameObject.GetComponent<DragDropScript>().CanDrag == true)
            {
                {
                    GameObject Copy_of_item = Instantiate(eventData.pointerDrag); //Esas drag etdiyimiz obyektin kopyasini yaradiriq.
                    Destroy(Copy_of_item.GetComponent<DragDropScript>());         //Yalniz visual olaraq goruntu lazim oldugu ucun scriptini silirik.
                    Copy_of_item.transform.SetParent(this.gameObject.GetComponent<RectTransform>());      //PArenti bu obyekt teyin edirik ki, tam merkezine yerlessin.
                    Copy_of_item.name = eventData.pointerDrag.transform.parent.GetComponentsInChildren<Text>()[1].text;
                    Copy_of_item.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;      //Hazirki slotun positionuna bizim itemi yerlesdirmek ucun istifade edilir.
                    IsEmpty = false;
                    Debug.Log("slot is full");
                }
            }
        }
    }

    
    
}
