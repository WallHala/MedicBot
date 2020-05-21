using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Bu scripti hereket etdirilecek elementin gameobjectine yerlesdiririk. Bu script sadece elementi drag etmek ucun istifade edilir.
/// IPointerDownHandler- sicanin pointerini hansisa obyekt ustunde basan zaman istifade olunan metodlari dasiyir.
/// IBeginDragHandler - Obyekti surutlemeye basladiqda bu classin icindeki metodlari istifade ede bilerik.
/// IEndDragHandler- obyekti surutlemeyi buraxdiqda bu classin icindeki metodlari istifade ede bilerik.
/// </summary>
public class DragDropScript : MonoBehaviour,IPointerDownHandler ,IBeginDragHandler, IEndDragHandler ,IDragHandler
{
    public bool CanDrag = false;
    private void Start()
    {
        gameObject.name = gameObject.transform.parent.GetComponentsInChildren<Text>()[1].text.ToLower();  //Butun obyektlerin adini gameObjectin uzerine yaziriq ki, CraftingSystem scriptde resultu tapa bilek. 
    }


    #region Commented code
    //private RectTransform ObjectRectTransform;    //Hemin element Canvas elementi oldugu ucun rectTransform vasitesile onu hereket etdireceyik.
    private GameObject ImageCopy;
    [SerializeField] Canvas InventoryCanvas;        //Ekrandaki olcu ferqlerine gore mouse-un hereketini tenzimlemek ucun bu itemin icinde oldugu Canvasi istifade etmeliyik.

    public void OnPointerDown(PointerEventData eventData)                     //Eger obyektin ustunde mouse-un sag duymesine bassaq bu funksiya ise dusecek.
    {
        Debug.Log("Pressed");

        if(byte.Parse(transform.parent.GetComponentInChildren<Text>().text) > 0)
        {
            CanDrag =true;
        }
        else
        {
            CanDrag = false;
        }
    }

    
    public void OnBeginDrag(PointerEventData eventData)                       //Obyekti hereket etdirmeye basladigimiz an bu funskiya ise dusecek.
    {
        if (CanDrag)
        {
            Debug.Log("Begin Dragging");
            ImageCopy = Instantiate(new GameObject(), this.transform.parent.transform);     //Obyektin visual seklinin kopyasini yaradiriq ki, goruntu olsun.
            ImageCopy.name = name;
            ImageCopy.AddComponent<Image>().sprite = GetComponent<Image>().sprite;
            ImageCopy.GetComponent<Image>().preserveAspect = true;
            ImageCopy.GetComponent<Image>().raycastTarget = false;

            transform.parent.GetComponentInChildren<Text>().text = (byte.Parse(transform.parent.GetComponentInChildren<Text>().text) - 1).ToString();
        }
        
        
    }




    public void OnDrag(PointerEventData eventData)      //Ekran boyu hereket etdirmeye davam etdikce bu funksiya isleyecek.
    {
        if (CanDrag)
        {
            ///
            /// RectTransform.anchoredPosition- obyektin pivot noqtesinin(Canvas obyektinin) Anchorlara gore positionunu gosterir.
            /// PointerEventData.delta- mouse-un hereket positionlarinin deyismesini gosterir. Buna gore de asagidaki dusturda + ile image-in positionlarinin ustune gelirik.
            /// Canvas.ScaleFactor- canvasin olcu gostericisinden vahid olcunu cixdigimiz zaman qalan hissedir.(CanvasScale.x ve ya y -1 =scaleFactor
            ///
            Debug.Log(eventData.delta);
            ImageCopy.GetComponent<RectTransform>().anchoredPosition += eventData.delta / InventoryCanvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)   //Hereket etdirmeyi bitirdiyimiz an bu funksiya ise dusecek.
    {
        if (CanDrag)
        {
            Debug.Log("Drag ended");
            Destroy(ImageCopy);
            Destroy(GameObject.Find("New Game Object"));
        }
    }
    #endregion




}
